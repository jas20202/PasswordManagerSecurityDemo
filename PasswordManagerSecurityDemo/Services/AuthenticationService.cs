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
        private static HttpContext httpContext => new HttpContextAccessor().HttpContext;
        public AuthenticationService(ApplicationDbContext applicationDbContext) {
            context = applicationDbContext;
        }

        public async Task<bool> LoginAsync(string username, string password) {
            User? user = context.Database.SqlQuery<User>($"SELECT * FROM Users WHERE username='{username}' AND password='{password}'").FirstOrDefault();
            if (user == null) throw new ArgumentException("User not found");

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

            return true;
        }
    }
}
