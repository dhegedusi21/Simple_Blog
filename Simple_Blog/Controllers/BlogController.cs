using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_Blog.Data;
using Simple_Blog.Models;
using System.Security.Claims;

namespace Simple_Blog.Controllers {
    public class BlogController : Controller {
        private readonly ILogger<UserController> _logger;
        private readonly BlogDbContext _context;

        public BlogController(ILogger<UserController> logger, BlogDbContext context) {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() {
            var posts = _context.Blog_posts.Include(p => p.User).ToList();
            return View(posts);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog_post post) {
            post.Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;

            _context.Blog_posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
