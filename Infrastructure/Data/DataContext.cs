using Microsoft.EntityFrameworkCore;
using diPasswords.Domain.Models;

namespace diPasswords.Infrastructure.Data
{
    /// <summary>
    /// Database context keeping encrypted data of certain user
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<MasterData> Users { get; set; } // Database set keeping master user data
        public DbSet<EncryptedData> Passwords { get; set; } // Database set keeping encrypted data of certain user

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
