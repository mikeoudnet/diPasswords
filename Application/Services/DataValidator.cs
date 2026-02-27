using diPasswords.Application.Interfaces;
using System.Text.RegularExpressions;

namespace diPasswords.Application.Services
{
    // Проверка вводимых данных
    public class DataValidator : IDataValidator
    {
        private readonly char[] _phoneSymbols = new char[] { '+', '-', '(', ')', ' ' }; // Допустимые символы при вводе номера телефона

        private ILogger _logger; // Логгирование работы программы в отдельный элемент
        private IDataBaseManager _dataBaseManager; // Свернутые запросы к базам данных

        public DataValidator(
            ILogger logger,
            IDataBaseManager dataBaseManager)
        {
            _logger = logger;
            _dataBaseManager = dataBaseManager;
        }

        //// Является ли имя уникальным
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
        // Являются ли поля логина и пароля пустыми
        public bool IsLoginAndPasswordNotEmpty(string login, string password)
        {
            if (login != "" && password != "") return true;
            else
            {
                _logger.CurrentError("Login and password fields cannot be empty");

                return false;
            }
        }
        // Введен ли e-mail корректно
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
        // Введен ли номер телефона корректно
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
