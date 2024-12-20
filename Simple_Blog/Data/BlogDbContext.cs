using Microsoft.EntityFrameworkCore;
using Simple_Blog.Models;

namespace Simple_Blog.Data {
    public class BlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base (options)
        {
            
        }

    }
}
