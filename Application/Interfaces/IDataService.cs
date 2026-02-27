using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// User data interaction
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Current user setting
        /// </summary>
        /// <param name="baseLogin"></param>
        void SetCurrentUser(string baseLogin);
        /// <summary>
        /// Adding new data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="favorite"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="description"></param>
        void AddData(string name, bool favorite, string login, string password, string email, string phone, string description);
        /// <summary>
        /// Edditting current data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="favorite"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="description"></param>
        void EditData(string name, bool favorite, string login, string password, string email, string phone, string description);
        /// <summary>
        /// Deleting the data
        /// </summary>
        /// <param name="name"></param>
        void DeleteData(string name);
        /// <summary>
        /// All user data getting
        /// </summary>
        /// <returns></returns>
        List<Data> GetData();
        /// <summary>
        /// Choosed user data getting
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Data GetSelectedData(string name);
    }
}
