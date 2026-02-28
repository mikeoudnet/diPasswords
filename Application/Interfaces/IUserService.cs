using diPasswords.Domain.Enums;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Database working responsible for user logins and passwords keeping
    /// </summary>
    public interface IUserService
    {
        bool IsPasswordCorrect(string login, string password);
        /// <summary>
        /// Adding new user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Add(string login, string password);
        /// <summary>
        /// User existing checking
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool IsUserExists(string login);
        /// <summary>
        /// Deleting the user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Delete(string login, string password);
        /// <summary>
        /// User password confirming
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ConfirmitionState Confirm(string? text = null);
    }
}
