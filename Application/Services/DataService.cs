using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using Microsoft.Data.SqlClient;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IDataService"/>
    // User data interaction
    public class DataService : IDataService
    {
        private string _baseLogin; // Current user

        private IDataBaseManager _dataBaseManager; // Collapsed databases requests
        private ILogger _logger; // Logging to separate element
        private IEncrypter _encrypter; // Data encrypting and decrypting

        public DataService(IDataBaseManager dataBaseManager, ILogger logger, IEncrypter encrypter)
        {
            _dataBaseManager = dataBaseManager;
            _logger = logger;
            _encrypter = encrypter;
        }

        /// <inheritdoc cref="IDataService.SetCurrentUser(string)"/>
        // Current user setting
        public void SetCurrentUser(string baseLogin) => _baseLogin = baseLogin;
        /// <inheritdoc cref="IDataService.AddData(string, bool, string, string, string, string, string)"/>
        // Adding new data
        public void AddData(string name, bool favorite, string login, string password, string email, string phone, string description)
        {
            if (_baseLogin != null)
            {
                _dataBaseManager.Request
                (
                    "USE diPasswords;" +
                    "INSERT INTO passwords (baseLogin, name) " +
                    "VALUES (@baseLogin, @name);",
                    new Dictionary<string, object>()
                    {
                        { "@baseLogin", _baseLogin },
                        { "@name", name }
                    }
                );                

                this.EditData(name, favorite, login, password, email, phone, description);
            }
            else _logger.Fatal("It is unknown whats user add data");
        }
        /// <inheritdoc cref="IDataService.EditData(string, bool, string, string, string, string, string)"/>
        // Edditting current data
        public void EditData(string name, bool favorite, string login, string password, string email, string phone, string description)
        {
            if (_baseLogin != null)
            {
                Data data = new Data();
                data.Name = name;
                data.Favorite = favorite;
                data.Login = login;
                data.Password = password;
                data.Email = email;
                data.Phone = phone;
                data.Description = description;

                _encrypter.SetIV(name);
                EncryptedData enData = _encrypter.Code(data);
                _dataBaseManager.Request
                (
                    "USE diPasswords;" +
                    "UPDATE passwords SET " +
                        "favorite = @favorite," +
                        "login = @login," +
                        "password = @password," +
                        "email = @email," +
                        "phone = @phone," +
                        "description = @description " +
                        "WHERE baseLogin = @baseLogin AND name = @name",
                    new Dictionary<string, object>()
                    {
                        { "@baseLogin", _baseLogin },
                        { "@name", enData.Name },
                        { "@favorite", enData.Favorite },
                        { "@login", enData.EncryptedLogin },
                        { "@password", enData.EncryptedPassword },
                        { "@email", enData.EncryptedEmail },
                        { "@phone", enData.EncryptedPhone },
                        { "@description", enData.EncryptedDescription }
                    }
                );
            }
        }
        /// <inheritdoc cref="IDataService.DeleteData(string)"/>
        // Deleting the data
        public void DeleteData(string name)
        {
            _dataBaseManager.Request
            (
                $"USE diPasswords;" +
                $"DELETE FROM passwords WHERE baseLogin = @baseLogin AND name = @name;",
                new Dictionary<string, object>
                {
                    { "@baseLogin", _baseLogin },
                    { "@name", name }
                }
            );
        }
        /// <inheritdoc cref="IDataService.GetData()"/>
        public List<Data> GetData()
        {
            List<EncryptedData> enDataList = _dataBaseManager.SelectData
            (
                $"USE diPasswords;" +
                $"SELECT * FROM passwords WHERE baseLogin = @baseLogin ORDER BY favorite DESC, name ASC;",
                new Dictionary<string, object>
                {
                    { "@baseLogin", _baseLogin }
                }
            );

            if  (enDataList != null)
            {
                List<Data> dataList = new List<Data>();
                for (int i = 0; i < enDataList.Count(); i++) dataList.Add(_encrypter.Decode(enDataList[i]));

                return dataList;
            }
            else return null;
        }
        /// <inheritdoc cref="IDataService.GetSelectedData(string)"/>
        // All user data getting
        public Data GetSelectedData(string name)
        {
            List<EncryptedData> enDataList = _dataBaseManager.SelectData
            (
                $"USE diPasswords;" +
                $"SELECT * FROM passwords WHERE baseLogin = @baseLogin AND name = @name;",
                new Dictionary<string, object>
                {
                    { "@baseLogin", _baseLogin },
                    { "@name", name }
                }
            );

            List<Data> dataList = new List<Data>();
            for (int i = 0; i < enDataList.Count(); i++) dataList.Add(_encrypter.Decode(enDataList[i]));

            return (dataList.Count > 0) ? dataList[0] : null;
        }
    }
}
