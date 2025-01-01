using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_Blog.Data;
using Simple_Blog.Models;
using System.Security.Claims;

namespace Simple_Blog.Controllers {
    [Authorize]

    public class BlogController : Controller {

        private readonly ILogger<UserController> _logger;
        private readonly BlogDbContext _context;

        public BlogController(ILogger<UserController> logger, BlogDbContext context) {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchString) {
            IQueryable<Blog_post> query = _context.Blog_posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User);

            if (!String.IsNullOrEmpty(searchString)) {
                query = query.Where(p => p.Title.ToLower().Contains(searchString.ToLower()));
            }

            var posts = query.ToList();
            return View(posts);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Blog_post post) {
            post.User_Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;

            _context.Blog_posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) {
            var post = _context.Blog_posts.Find(id);
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (post.User_Id != currentUserId) {
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(Blog_post post) {
            post.UpdatedAt = DateTime.Now;

            _context.Blog_posts.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id){
            var post = _context.Blog_posts
                       .Include(p => p.Comments)
                       .FirstOrDefault(p => p.Id == id);

            if (post != null)
            {
                _context.Comments.RemoveRange(post.Comments);
                _context.Blog_posts.Remove(post);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddComment(int blogpostId, string content) {
            var comment = new Comment {
                Content = content,
                Blogpost_Id = blogpostId,
                User_Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteComment(int id) {
            var comment = _context.Comments.Find(id);

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditComment(int id) {
            var comment = _context.Comments.Find(id);

            return View(comment);
        }

        [HttpPost]
        public IActionResult EditCommentSave(Comment comment) {
            comment.UpdatedAt = DateTime.Now;

            _context.Comments.Update(comment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
