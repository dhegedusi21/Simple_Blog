using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Simple_Blog.Data;
using Simple_Blog.Models;
using System.Diagnostics;
using System.Security.Claims;

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

        public async Task<IActionResult> LoginUser(User user) {
            var userInDb = _context.Users.FirstOrDefault(u =>
            u.Email == user.Email &&
            u.Password == user.Password);

            if (userInDb == null) {
                ViewBag.Error = "Invalid email or password";
                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInDb.Username),
                new Claim(ClaimTypes.NameIdentifier, userInDb.Id.ToString())
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Blog");
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }



        public IActionResult Register() {
            return View();
        }
        public IActionResult RegisterUser(User user) {

            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) {
                ViewBag.Error = "All fields are required!";
                return View("Register");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful! Please log in.";
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
