using diPasswords.Application.Interfaces;
using diPasswords.Domain.Enums;
using diPasswords.Domain.Models;
using diPasswords.Infrastructure.Data;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IUserService"/>
    // Database working responsible for user logins and passwords keeping
    public class UserService : IUserService
    {
        private string? _confirmer = null; // Password to confirm it

        private DataContext _dataContext; // Common database context

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <inheritdoc cref="IUserService.IsPasswordCorrect(string, string)"/>
        // Correct existing password checking
        public bool IsPasswordCorrect(string login, string password)
        {
            List<MasterData> data = _dataContext.Users.Where(x => x.Login == login).ToList();

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
            bool isLoginExists = _dataContext.Users.Any(x => x.Login == login);
            if (!isLoginExists)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);

                _dataContext.Users.Add(new MasterData { Login = login, Password = hashed });
                _dataContext.SaveChanges();

                return true;
            }
            else return false;
        }
        /// <inheritdoc cref="IUserService.IsUserExists(string)"/>
        // User existing checking
        public bool IsUserExists(string login) => _dataContext.Users.Any(x => x.Login == login);
        /// <inheritdoc cref="IUserService.Delete(string, string)"/>
        // Deleting the user
        public bool Delete(string login, string password)
        {
            bool isUserCorrect = this.IsPasswordCorrect(login, password);

            if (isUserCorrect)
            {
                MasterData userToRemove = _dataContext.Users.Where(x => x.Login == login).ToList()[0];
                _dataContext.Users.Remove(userToRemove);
                _dataContext.SaveChanges();

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
