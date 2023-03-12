using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManagerSecurityDemo.Models.Entities;
using PasswordManagerSecurityDemo.Services;
using System.Security.Claims;

namespace PasswordManagerSecurityDemo.Controllers {
    [Authorize]
    public class ContentController: Controller {
        private readonly ContentService contentService;
        public ContentController(ContentService contentService) {
            this.contentService = contentService;
        }

        public IActionResult Index() {
            string? userID = GetUserId();
            if (userID == null) {
                return Forbid();
            }
            
            return View(contentService.GetAllPasswordEntries(userID));
        }

        [Route("search")]
        public IActionResult Search(string name) {
            string? userID = GetUserId();
            if (userID == null) {
                return Forbid();
            }

            return View(contentService.SearchForPasswordEntry(name, userID));
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(PasswordEntry passwordEntry) {
            string? userID = GetUserId();
            if (userID == null) {
                return Forbid();
            }
            passwordEntry.UserID = userID;
            contentService.AddPasswordEntry(passwordEntry);
            return RedirectToAction("Index");
        }

        [Route("delete")]
        [HttpPost]
        public IActionResult Delete(string passwordEntryId) {
            string? userID = GetUserId();
            if (userID == null) {
                return Forbid();
            }
            contentService.RemovePasswordEntry(passwordEntryId, userID);
            return RedirectToAction("Index");
        }

        private string? GetUserId() {
            return HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
