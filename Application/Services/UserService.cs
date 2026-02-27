using diPasswords.Application.Interfaces;
using diPasswords.Domain.Enums;
using diPasswords.Domain.Models;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IUserService"/>
    // Database working responsible for user logins and passwords keeping
    public class UserService : IUserService
    {
        private IDataBaseManager _dataBaseManager; // Collapsed databases requests
        private string? _confirmer = null; // Password to confirm it

        public UserService(IDataBaseManager dataBaseManager)
        {
            _dataBaseManager = dataBaseManager;
        }

        /// <inheritdoc cref="IUserService.CreateDatabase()"/>
        // Database creating, if it isn't yet created
        public void CreateDatabase()
        {
            _dataBaseManager.Request
            (
                "IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'diPasswords')" +
                    "CREATE DATABASE diPasswords;"
            );
            _dataBaseManager.Request
            (
                "USE diPasswords;" +
                "IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'users')" +
                "BEGIN " +
                    "CREATE TABLE users " +
                    "(" +
                        "id INT PRIMARY KEY IDENTITY(1, 1)," +
                        "baseLogin VARCHAR(20) NOT NULL," +
                        "basePassword NVARCHAR(60) NOT NULL" +
                    ");" +
                "END;" +
                "" +
                "IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'passwords')" +
                "BEGIN " +
                    "CREATE TABLE passwords " +
                    "(" +
                        "id INT PRIMARY KEY IDENTITY(1, 1)," +
                        "baseLogin NVARCHAR(20) NOT NULL," +
                        "name NVARCHAR(30) NOT NULL," +
                        "favorite BIT NOT NULL DEFAULT 0," +
                        "login NVARCHAR(MAX) NULL," +
                        "password NVARCHAR(MAX) NULL," +
                        "email NVARCHAR(MAX) NULL," +
                        "phone VARCHAR(MAX) NULL," +
                        "description NVARCHAR(MAX) NULL," +
                        "iVector VARBINARY(16) NULL" +
                    ");" +
                "END;"
            );
        }
        /// <inheritdoc cref="IUserService.IsPasswordCorrect(string, string)"/>
        // Correct existing password checking
        public bool IsPasswordCorrect(string login, string password)
        {
            List<MasterData> data = _dataBaseManager.SelectBaseData
            (
                "USE diPasswords;" +
                "SELECT * FROM users WHERE baseLogin = @baseLogin;",
                new Dictionary<string, object>
                {
                    { "@baseLogin", login }
                }
            );

            // Trying to compare the passwords if login exists
            try
            {
                if (BCrypt.Net.BCrypt.Verify(password, data[0].Password)) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc cref="IUserService.Add(string, string)"/>
        // Adding new user
        public bool Add(string login, string password)
        {
            bool isLoginExists = Convert.ToBoolean(_dataBaseManager.GetCount
            (
                $"USE diPasswords;" +
                $"IF EXISTS (SELECT 1 FROM users WHERE baseLogin = @baseLogin) " +
                    $"SELECT 1 " +
                $"ELSE SELECT 0;",
                new Dictionary<string, object>()
                {
                    { "@baseLogin", login }
                }
            ));

            if (!isLoginExists)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);

                _dataBaseManager.Request
                (
                    $"USE diPasswords;" +
                    $"INSERT INTO users (baseLogin, basePassword) VALUES (@baseLogin, @basePassword);",
                    new Dictionary<string, object>()
                    {
                        { "@baseLogin", login },
                        { "@basePassword", hashed }
                    }
                );

                return true;
            }
            else return false;
        }
        /// <inheritdoc cref="IUserService.IsUserExists(string)"/>
        // User existing checking
        public bool IsUserExists(string login)
        {
            return Convert.ToBoolean(_dataBaseManager.GetCount
            (
                $"USE diPasswords;" +
                $"IF EXISTS (SELECT 1 FROM users WHERE baseLogin = @baseLogin)" +
                    $"SELECT 1;" +
                $"ELSE SELECT 0;",
                new Dictionary<string, object>()
                {
                    { "@baseLogin", login }
                }
            ));
        }
        /// <inheritdoc cref="IUserService.Delete(string, string)"/>
        // Deleting the user
        public bool Delete(string login, string password)
        {
            bool isUserCorrect = this.IsPasswordCorrect(login, password);

            if (isUserCorrect)
            {
                _dataBaseManager.Request
                (
                    $"USE diPasswords;" +
                    $"DELETE FROM users WHERE baseLogin = @baseLogin;" +
                    $"DELETE FROM passwords WHERE baseLogin = @baseLogin",
                    new Dictionary<string, object>()
                    {
                        { "@baseLogin", login }
                    }
                );

                return true;
            }
            else return false;
        }
        /// <inheritdoc cref="IUserService.Confirm(string?)"/>
        // User password confirming
        public ConfirmitionState Confirm(string? text = null)
        {
            if (text != null)
            {
                if (_confirmer == null)
                {
                    _confirmer = text;
                    return ConfirmitionState.NotStarted;
                }
                else
                {
                    if (text == _confirmer)
                    {
                        _confirmer = null;
                        return ConfirmitionState.Confirmed;
                    }
                    else return ConfirmitionState.Failed;
                }
            }
            else
            {
                _confirmer = null;
                return ConfirmitionState.NotStarted;
            }
        }
    }
}
