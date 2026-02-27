namespace diPasswords.Application.Interfaces
{
    // Проверка вводимых символов на соответствие локальным правилам
    public interface IValidator
    {
        bool IsKeyValid(char pressedKey); // Является ли вводимый символ допустимым
        bool IsLoginCorrect(char pressedKey); // Является ли логин корректным
    }
}
