using Microsoft.AspNetCore.Mvc;
using Simple_Blog.Models;
using System.Diagnostics;

namespace Simple_Blog.Controllers {
    public class UserController : Controller {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger) {
            _logger = logger;
        }

        public IActionResult allUsers() {
            return View();
        }

        public IActionResult login() {
            return View();
        }
        public IActionResult register() {
            return View();
        }
        public IActionResult registerUser() {
            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
