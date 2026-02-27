using diPasswords.Application.Interfaces;
using System.Text.RegularExpressions;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IDataValidator"/>
    // Inputting data validation
    public class DataValidator : IDataValidator
    {
        private readonly char[] _phoneSymbols = new char[] { '+', '-', '(', ')', ' ' }; // Acceptable symbols by phone number inputting

        private ILogger _logger; // Logging to separate element
        private IDataBaseManager _dataBaseManager; // Collapsed databases requests

        public DataValidator(
            ILogger logger,
            IDataBaseManager dataBaseManager)
        {
            _logger = logger;
            _dataBaseManager = dataBaseManager;
        }

        /// <inheritdoc cref="IDataValidator.IsNameUnique(string, string)"/>
        // Unique name checking
        public bool IsNameUnique(string baseLogin, string name)
        {
            {
                if (name == "")
                {
                    _logger.CurrentError("Name field cannot be empty");

                    return false;
                }
                else if (name.Contains("★"))
                {
                    _logger.CurrentError("Name field has invalid symbols");

                    return false;
                }

                bool isNameExists = Convert.ToBoolean(_dataBaseManager.GetCount
                (
                    "USE diPasswords;" +
                    "IF EXISTS (SELECT 1 FROM passwords WHERE baseLogin = @baseLogin AND name = @name)" +
                        "SELECT 1" +
                    "ELSE SELECT 0;",
                    new Dictionary<string, object>
                    {
                        { "@baseLogin", baseLogin },
                        { "@name", name }
                    }
                ));

                if (!isNameExists) return true;
                else
                {
                    _logger.CurrentError("New name has to be unique");

                    return false;
                }
            }
        }
        /// <inheritdoc cref="IDataValidator.IsLoginAndPasswordNotEmpty(string, string)"/>
        // Login and password checking (is it empty)
        public bool IsLoginAndPasswordNotEmpty(string login, string password)
        {
            if (login != "" && password != "") return true;
            else
            {
                _logger.CurrentError("Login and password fields cannot be empty");

                return false;
            }
        }
        /// <inheritdoc cref="IDataValidator.IsEmailCorrect(string)"/>
        // Correct e-mail checking
        public bool IsEmailCorrect(string email)
        {
            string pattern = @"^[\w\.-_]+@[\w\.-]+\.\w+$";

            if (Regex.IsMatch(email, pattern) || email == "") return true;
            else
            {
                _logger.CurrentError("Inputted e-mail isn't match with format");

                return false;
            }
        }
        /// <inheritdoc cref="IDataValidator.IsPhoneCorrect(string)"/>
        // Correct phone number checking
        public bool IsPhoneCorrect(string phone)
        {
            foreach (char c in phone)
            {
                if (!char.IsDigit(c) && !_phoneSymbols.Contains(c))
                {
                    _logger.CurrentError("Phone has to have only digits and symbols");

                    return false;
                }
            }
            return true;
        }
    }
}
