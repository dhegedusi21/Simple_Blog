using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Blog.Models {
    public class Comment {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int User_Id { get; set; }

        [ForeignKey("User_Id")]
        public User User { get; set; }

        public int Blogpost_Id { get; set; }

        [ForeignKey("Blogpost_Id")]
        public Blog_post Blog_post { get; set; }
    }
}
