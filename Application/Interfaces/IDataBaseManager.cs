using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    // Свернутые запросы к базам данных
    // (нет нужды заново прописывать однообразный код)
    public interface IDataBaseManager
    {
        void Request(string commandString, Dictionary<string, object>? sqlInjetion = null); // Запрос без вывода
        List<EncryptedData> SelectData(string commandString, Dictionary<string, object>? sqlInjetion = null); // Запрос с чтением и выводом
        List<MasterData> SelectBaseData(string commandString, Dictionary<string, object>? sqlInjetion = null); // Запрос с чтением и выводом для логина и пароля пользователя
        object GetCount(string commandString, Dictionary<string, object>? sqlInjetion = null); // Запрос с подсчетом по условию
    }
}
