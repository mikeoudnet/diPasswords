using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Collapsed databases requests
    /// (no neccessary to write same code)
    /// </summary>
    public interface IDataBaseManager
    {
        /// <summary>
        /// Request without outputting
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="sqlInjetion"></param>
        void Request(string commandString, Dictionary<string, object>? sqlInjetion = null);
        /// <summary>
        /// Request with user data reading and outputting
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="sqlInjetion"></param>
        /// <returns></returns>
        List<EncryptedData> SelectData(string commandString, Dictionary<string, object>? sqlInjetion = null);
        /// <summary>
        /// Request with base login and password reading and outputting
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="sqlInjetion"></param>
        /// <returns></returns>
        List<MasterData> SelectBaseData(string commandString, Dictionary<string, object>? sqlInjetion = null);
        /// <summary>
        /// Request with counting
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="sqlInjetion"></param>
        /// <returns></returns>
        object GetCount(string commandString, Dictionary<string, object>? sqlInjetion = null);
    }
}
