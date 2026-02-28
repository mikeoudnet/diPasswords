using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using diPasswords.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IEncrypter"/>
    // Data encrypting and decrypting
    public class Encrypter : IEncrypter
    {
        private byte[] _key = new byte[32]; // Key using non-hashing user password for encrypting

        private DataContext _dataContext; // Database context keeping encrypted data of certain user

        /// <summary>
        /// String encrypting by key and IV
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
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
        /// <summary>
        /// String decrypting by key and IV
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
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

        public Encrypter(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc cref="IEncrypter.PasswordToKey(string)"/>
        // Current password converting to bytes array
        public void PasswordToKey(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            for (int i = 0; i < 32; i++) _key[i] = (i < password.Length) ? passwordBytes[i] : (byte)0;
        }
        /// <inheritdoc cref="IEncrypter.SetIV(string)"/>
        // Initialization vector and its adding to database
        public void SetIV(string name)
        {
            byte[] iv;
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                iv = aes.IV;
            }

            var dataToEdit = _dataContext.Passwords.FirstOrDefault(x => x.Name == name);
            if (dataToEdit != null)
            {
                dataToEdit.IVector = iv;

                _dataContext.SaveChanges();
            }
        }
        /// <inheritdoc cref="IEncrypter.Code(Data)"/>
        // Data model coding
        public EncryptedData Code(Data data)
        {
            List<EncryptedData> findData = _dataContext.Passwords.Where(x => x.Name == data.Name).ToList();

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
        /// <inheritdoc cref="IEncrypter.Decode(EncryptedData)"/>
        // Encrypted data model decoding
        public Data Decode(EncryptedData enData)
        {
            List<EncryptedData> findData = _dataContext.Passwords.Where(x => x.Name == enData.Name).ToList();

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
