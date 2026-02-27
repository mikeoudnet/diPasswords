using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace diPasswords.Application.Services
{
    // Шифрование и дешифрование данных
    public class Encrypter : IEncrypter
    {
        private byte[] _key = new byte[32]; // Ключ, использующий нехэшированный пароль пользователя для шифрования

        private IDataBaseManager _dataBaseManager; // Свернутые запросы к базам данных

        // Шифрование строки по данному ключу и вектору иницализации
        private string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        // Дешифрование строки по данному ключу и вектору иницализации
        private string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public Encrypter(IDataBaseManager dataBaseManager)
        {
            _dataBaseManager = dataBaseManager;
        }

        // Конвертирование текущего пароля в массив байтов
        public void PasswordToKey(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            for (int i = 0; i < 32; i++) _key[i] = (i < password.Length) ? passwordBytes[i] : (byte)0;
        }
        // Создание вектора инициализации и добавление его в базу данных
        public void SetIV(string name)
        {
            byte[] iv;
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                iv = aes.IV;
            }

            _dataBaseManager.Request
            (
                "USE diPasswords;" +
                "UPDATE passwords SET " +
                    "iVector = @iVector " +
                    "WHERE name = @name;",
                new Dictionary<string, object>()
                {
                    { "@iVector", iv },
                    { "@name", name }
                }
            );
        }
        // Кодировка модели данных
        public EncryptedData Code(Data data)
        {
            List<EncryptedData> findData = _dataBaseManager.SelectData
            (
                "USE diPasswords;" +
                "SELECT * FROM passwords WHERE name = @name",
                new Dictionary<string, object>()
                {
                    { "@name", data.Name }
                }
            );
            byte[] iv = (findData == null) ? null : findData[0].IVector;
            if (findData != null)
            {
                EncryptedData enData = new EncryptedData();
                enData.Name = data.Name;
                enData.Favorite = data.Favorite;
                enData.EncryptedLogin = EncryptString(data.Login, _key, iv);
                enData.EncryptedPassword = EncryptString(data.Password, _key, iv);
                enData.EncryptedEmail = EncryptString(data.Email, _key, iv);
                enData.EncryptedPhone = EncryptString(data.Phone, _key, iv);
                enData.EncryptedDescription = EncryptString(data.Description, _key, iv);

                return enData;
            }
            else return null;
        }
        // Декодировка модели зашифрованных данных
        public Data Decode(EncryptedData enData)
        {
            List<EncryptedData> findData = _dataBaseManager.SelectData
            (
                "USE diPasswords;" +
                "SELECT * FROM passwords WHERE name = @name",
                new Dictionary<string, object>()
                {
                    { "@name", enData.Name }
                }
            );
            byte[] iv = (findData == null) ? null : findData[0].IVector;
            if (iv != null)
            {
                Data data = new Data();
                data.Name = enData.Name;
                data.Favorite = enData.Favorite;
                data.Login = DecryptString(enData.EncryptedLogin, _key, iv);
                data.Password = DecryptString(enData.EncryptedPassword, _key, iv);
                data.Email = DecryptString(enData.EncryptedEmail, _key, iv);
                data.Phone = DecryptString(enData.EncryptedPhone, _key, iv);
                data.Description = DecryptString(enData.EncryptedDescription, _key, iv);

                return data;
            }
            else return null;
        }
    }
}
