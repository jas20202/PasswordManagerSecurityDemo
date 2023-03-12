using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManagerSecurityDemo.Models.Entities {
    public class PasswordEntry {
        [Key]
        public required string Id { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
