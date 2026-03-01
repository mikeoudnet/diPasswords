using System.ComponentModel.DataAnnotations;

namespace diPasswords.Domain.Models
{
    /// <summary>
    /// Model of decrypted data from database
    /// </summary>
    public class Data
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string BaseLogin { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public bool Favorite { get; set; } = false;
        [MaxLength(30)]
        public string Login { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(16)]
        public byte[] IVector { get; set; }

    }
}
