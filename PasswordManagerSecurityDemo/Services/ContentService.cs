using Microsoft.EntityFrameworkCore;
using PasswordManagerSecurityDemo.Models.DataStore;
using PasswordManagerSecurityDemo.Models.Entities;

namespace PasswordManagerSecurityDemo.Services {
    public class ContentService {
        private readonly ApplicationDbContext context;
        public ContentService(ApplicationDbContext applicationDbContext) {
            context = applicationDbContext;
        }

        public string AddPasswordEntry(PasswordEntry passwordEntry) {
            passwordEntry.Id = Guid.NewGuid().ToString();
            
            context.PasswordEntries.Add(passwordEntry);
            context.SaveChanges();

            return passwordEntry.Id;
        }

        public bool RemovePasswordEntry(string passwordEntryId, string userId) {
            var passwordEntry = context.PasswordEntries.Find(passwordEntryId);
            
            if (passwordEntry == null || !passwordEntry.UserID.Equals(userId)) {
                throw new ArgumentException("There was nothing found to delete.");
            }

            context.PasswordEntries.Remove(passwordEntry);
            return context.SaveChanges() > 0;
        }

        public List<PasswordEntry> GetAllPasswordEntries(string userId) {
            return context.PasswordEntries.FromSqlRaw($"SELECT * FROM PasswordEntries WHERE userid='{userId}'").ToList();
        }

        public List<PasswordEntry> SearchForPasswordEntry(string name, string userId) {
            return context.PasswordEntries.FromSqlRaw($"SELECT * FROM PasswordEntries WHERE userid='{userId}' AND name like '{name}%'").ToList();
        }
    }
}
