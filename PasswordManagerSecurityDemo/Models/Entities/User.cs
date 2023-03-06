using System.ComponentModel.DataAnnotations;

namespace PasswordManagerSecurityDemo.Models.Entities {
    public class User {
        [Key]
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
