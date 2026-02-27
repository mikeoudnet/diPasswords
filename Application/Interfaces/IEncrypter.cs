using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Data encrypting and decrypting
    /// </summary>
    public interface IEncrypter
    {
        /// <summary>
        /// Current password converting to bytes array
        /// </summary>
        /// <param name="password"></param>
        void PasswordToKey(string password);
        /// <summary>
        /// Initialization vector and its adding to database
        /// </summary>
        /// <param name="name"></param>
        void SetIV(string name);
        /// <summary>
        /// Data model coding
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        EncryptedData Code(Data data);
        /// <summary>
        /// Encrypted data model decoding
        /// </summary>
        /// <param name="enData"></param>
        /// <returns></returns>
        Data Decode(EncryptedData enData);
    }
}
