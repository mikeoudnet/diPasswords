using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using diPasswords.Infrastructure.Data;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IDataService"/>
    // User data interaction
    public class DataService : IDataService
    {
        private string _baseLogin; // Current user

        private DataContext _dataContext; // Database context keeping encrypted data of certain user
        private IDataBaseManager _dataBaseManager; // Collapsed databases requests
        private ILogger _logger; // Logging to separate element
        private IEncrypter _encrypter; // Data encrypting and decrypting

        public DataService(DataContext dataContext, ILogger logger, IEncrypter encrypter)
        {
            _dataContext = dataContext;
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
                _dataContext.Passwords.Add(new EncryptedData { BaseLogin = _baseLogin, Name = name });
                _dataContext.SaveChanges();

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
                var dataToEdit = _dataContext.Passwords.FirstOrDefault(x => x.Name == name);
                if (dataToEdit != null)
                {
                    dataToEdit.Favorite = enData.Favorite;
                    dataToEdit.EncryptedLogin = enData.EncryptedLogin;
                    dataToEdit.EncryptedPassword = enData.EncryptedPassword;
                    dataToEdit.EncryptedEmail = enData.EncryptedEmail;
                    dataToEdit.EncryptedPhone = enData.EncryptedPhone;
                    dataToEdit.EncryptedDescription = enData.EncryptedDescription;

                    _dataContext.SaveChanges();
                }
            }
        }
        /// <inheritdoc cref="IDataService.DeleteData(string)"/>
        // Deleting the data
        public void DeleteData(string name)
        {
            EncryptedData dataToRemove = _dataContext.Passwords.Where(x => x.Name == name && x.BaseLogin == _baseLogin).ToList()[0];
            _dataContext.Passwords.Remove(dataToRemove);
            _dataContext.SaveChanges();
        }
        /// <inheritdoc cref="IDataService.GetData()"/>
        // All user data getting
        public List<Data> GetData()
        {
            List<EncryptedData> enDataList = _dataContext.Passwords.OrderBy(x => x.Favorite).OrderBy(x => x.Name).ToList();
            if (enDataList != null)
            {
                List<Data> dataList = new List<Data>();
                for (int i = 0; i < enDataList.Count; i++) dataList.Add(_encrypter.Decode(enDataList[i]));

                return dataList;
            }
            else return null;
        }
        /// <inheritdoc cref="IDataService.GetSelectedData(string)"/>
        // All user data getting
        public Data GetSelectedData(string name)
        {
            List<EncryptedData> enDataList = _dataContext.Passwords.Where(x => x.BaseLogin == _baseLogin && x.Name == name).ToList();

            List<Data> dataList = new List<Data>();
            for (int i = 0; i < enDataList.Count(); i++) dataList.Add(_encrypter.Decode(enDataList[i]));

            return (dataList.Count > 0) ? dataList[0] : null;
        }
    }
}
