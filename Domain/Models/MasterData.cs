using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diPasswords.Domain.Models
{
    /// <summary>
    /// Model of master data from database
    /// </summary>
    [Table("users")]
    public class MasterData
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        public string Login { get; set; }
        [MaxLength(60)]
        public string Password { get; set; }
    }
}
