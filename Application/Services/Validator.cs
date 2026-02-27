using diPasswords.Application.Interfaces;

namespace diPasswords.Application.Services
{
    /// <inheritdoc cref="IValidator"/>
    // Inputting symbols checking for local rules appropriating
    public class Validator : IValidator
    {
        /// <inheritdoc cref="IValidator.IsKeyValid(char)"/>
        // Valid key checking
        public bool IsKeyValid(char pressedKey)
        {
            if (pressedKey >= 'A' && pressedKey <= 'Z' || pressedKey >= 'a' && pressedKey <= 'z' || char.IsDigit(pressedKey) || pressedKey == '-' || pressedKey == '_' || char.IsControl(pressedKey))
            {
                return true;
            }
            else return false;
        }

        /// <inheritdoc cref="IValidator.IsLoginCorrect(char)"/>
        // Correct login checking
        public bool IsLoginCorrect(char keyPressed)
        {
            if (char.IsDigit(keyPressed)) return false;
            else return true;
        }
    }
}
