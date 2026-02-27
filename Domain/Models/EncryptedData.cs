namespace diPasswords.Domain.Models
{
    /// <summary>
    /// Model of encrypted data from database
    /// </summary>
    public class EncryptedData
    {
        public string Name { get; set; } = "";
        public bool Favorite { get; set; } = false;
        public string EncryptedLogin { get; set; } = "";
        public string EncryptedPassword { get; set; } = "";
        public string EncryptedEmail { get; set; } = "";
        public string EncryptedPhone { get; set; } = "";
        public string EncryptedDescription { get; set; } = "";
        public byte[] IVector { get; set; }
    }
}
