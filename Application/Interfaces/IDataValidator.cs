namespace diPasswords.Application.Interfaces
{
    // Проверка вводимых данных
    public interface IDataValidator
    {
        bool IsNameUnique(string baseLogin, string name); // Является ли имя уникальным
        bool IsLoginAndPasswordNotEmpty(string login, string password); // Являются ли поля логина и пароля пустыми
        bool IsEmailCorrect(string email); // Введен ли e-mail корректно
        bool IsPhoneCorrect(string phone); // Введен ли номер телефона корректно
    }
}
