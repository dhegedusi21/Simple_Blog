using System.ComponentModel.DataAnnotations;

namespace Simple_Blog.Models {
    public class Users {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

    }
}
