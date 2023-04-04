using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using PasswordManagerSecurityDemo.Models.DataStore;
using PasswordManagerSecurityDemo.Models.Entities;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace PasswordManagerSecurityDemo.Services {
    public class AuthenticationService {
        private readonly ApplicationDbContext context;
        
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthenticationService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor) {
            context = applicationDbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(string username, string password) {
            var query = $"SELECT * FROM Users WHERE Username='{username}' AND Password='{password}'";
            User? user = context.Users.FromSqlRaw(query).FirstOrDefault();
            if (user == null) throw new ArgumentException("User not found");

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContextAccessor.HttpContext!.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

            return true;
        }

        public async Task LogoutAsync() {
            await httpContextAccessor.HttpContext!.SignOutAsync();
        }
    }
}
