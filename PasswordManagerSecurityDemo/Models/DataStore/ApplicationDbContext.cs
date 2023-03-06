using Microsoft.EntityFrameworkCore;
using PasswordManagerSecurityDemo.Models.Entities;

namespace PasswordManagerSecurityDemo.Models.DataStore {
    public class ApplicationDbContext: DbContext{
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordEntry> PasswordEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {}
    }
}
