using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    public interface IEncrypter
    {
        void PasswordToKey(string password); // Конвертирование текущего пароля в массив байтов
        void SetIV(string name); // Создание вектора инициализации и добавление его в базу данных
        EncryptedData Code(Data data); // Кодировка модели данных
        Data Decode(EncryptedData enData); // Декодировка модели зашифрованных данных
    }
}
