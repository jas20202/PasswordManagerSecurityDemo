using Microsoft.AspNetCore.Mvc;
using PasswordManagerSecurityDemo.Models.Entities;
using PasswordManagerSecurityDemo.Services;
using System.Globalization;

namespace PasswordManagerSecurityDemo.Controllers {
    [Route("auth")]
    public class AuthenticationController: Controller {
        private readonly AuthenticationService authenticationService;
        public AuthenticationController(AuthenticationService authenticationService) {
            this.authenticationService = authenticationService;
        }

        public IActionResult Index() { 
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password) { 
            await authenticationService.LoginAsync(username, password);
            return RedirectToAction("Index", "Content");
        }
    }
}
