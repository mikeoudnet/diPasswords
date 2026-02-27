namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Inputting data validation
    /// </summary>
    public interface IDataValidator
    {
        /// <summary>
        /// Unique name checking
        /// </summary>
        /// <param name="baseLogin"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool IsNameUnique(string baseLogin, string name);
        /// <summary>
        /// Login and password checking (is it empty)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsLoginAndPasswordNotEmpty(string login, string password);
        /// <summary>
        /// Correct e-mail checking
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailCorrect(string email);
        /// <summary>
        /// Correct phone number checking
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool IsPhoneCorrect(string phone);
    }
}
