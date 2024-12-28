using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Simple_Blog.Models {
    public class Blog_post {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }


        public int User_Id { get; set; }


        [ForeignKey("User_Id")]
        public User User { get; set; }
    }
}
