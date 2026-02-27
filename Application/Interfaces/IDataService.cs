using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    // Взаимодействие с данными пользователя
    public interface IDataService
    {
        void SetCurrentUser(string baseLogin); // Установить нынешнего пользователя
        void AddData(string name, bool favorite, string login, string password, string email, string phone, string description); // Добавить данные
        void EditData(string name, bool favorite, string login, string password, string email, string phone, string description); // Изменить данные
        void DeleteData(string name); // Удалить данные
        List<Data> GetData(); // Получить все данные пользователя
        Data GetSelectedData(string name); // Получить выбранные данные пользователя
    }
}
