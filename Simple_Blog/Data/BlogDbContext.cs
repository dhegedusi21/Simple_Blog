using Microsoft.EntityFrameworkCore;
using Simple_Blog.Models;

namespace Simple_Blog.Data {
    public class BlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog_post> Blog_posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base (options)
        {
            
        }


    }
}
