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
        /// <param name="data"></param>
        void AddData(Data data);
        /// <summary>
        /// Editting current data
        /// </summary>
        /// <param name="data"></param>
        void EditData(Data data);
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
