namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Inputting symbols checking for local rules appropriating
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Valid key checking
        /// </summary>
        /// <param name="pressedKey"></param>
        /// <returns></returns>
        bool IsKeyValid(char pressedKey);
        /// <summary>
        /// Correct login checking
        /// </summary>
        /// <param name="pressedKey"></param>
        /// <returns></returns>
        bool IsLoginCorrect(char pressedKey);
    }
}
