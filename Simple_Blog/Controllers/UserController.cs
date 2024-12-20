using Microsoft.AspNetCore.Mvc;
using Simple_Blog.Data;
using Simple_Blog.Models;
using System.Diagnostics;

namespace Simple_Blog.Controllers {
    public class UserController : Controller {
        private readonly ILogger<UserController> _logger;
        private readonly BlogDbContext _context;

        public UserController(ILogger<UserController> logger, BlogDbContext context) {
            _logger = logger;
            _context = context;
        }

        public IActionResult UserOverview() {
            var allUsers = _context.Users.ToList();
            return View(allUsers);
        }

        public IActionResult Login() {
            return View();
        }
        public IActionResult Register() {
            return View();
        }
        public IActionResult RegisterUser(User user) {

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Delete(int id) {

            var userInDb = _context.Users.SingleOrDefault(x => x.Id == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return RedirectToAction("UserOverview");
        }

        public IActionResult EditUser (int id) {
            var userInDb = _context.Users.SingleOrDefault(x => x.Id == id);
            return View(userInDb);

        }

        public IActionResult SaveEdit(User user) {
            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("UserOverview");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
