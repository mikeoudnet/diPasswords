namespace diPasswords.Domain.Models
{
    /// <summary>
    /// Model of decrypted data from database
    /// </summary>
    public class Data
    {
        public string Name { get; set; } = "";
        public bool Favorite { get; set; } = false;
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Description { get; set; } = "";
        public byte[] IVector { get; set; }

    }
}
