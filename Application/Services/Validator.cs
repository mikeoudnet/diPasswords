using diPasswords.Application.Interfaces;

namespace diPasswords.Application.Services
{
    // Проверка вводимых символов на соответствие локальным правилам
    public class Validator : IValidator
    {
        // Является ли вводимый символ допустимым
        public bool IsKeyValid(char pressedKey)
        {
            if (pressedKey >= 'A' && pressedKey <= 'Z' || pressedKey >= 'a' && pressedKey <= 'z' || char.IsDigit(pressedKey) || pressedKey == '-' || pressedKey == '_' || char.IsControl(pressedKey))
            {
                return true;
            }
            else return false;
        }

        public bool IsLoginCorrect(char keyPressed) // Является ли логин корректным
        {
            if (char.IsDigit(keyPressed)) return false;
            else return true;
        }
    }
}
