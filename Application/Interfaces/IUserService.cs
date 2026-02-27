using diPasswords.Domain.Enums;

namespace diPasswords.Application.Interfaces
{
    // Работа с базой данных, отвечающей за хранение логинов и паролей пользователей
    public interface IUserService
    {
        void CheckExisting(); // Проверить существование базы данных
        bool IsPasswordCorrect(string login, string password); // Проверка на совпадения пароля по пользователю
        bool Add(string login, string password); // Добавить нового пользователя
        bool IsUserExists(string login); // Существует ли данный пользователь
        bool Delete(string login, string password); // Удалить данного пользователя
        ConfirmitionState Confirm(string? text = null); // Подтвердить пароль пользователя
    }
}
