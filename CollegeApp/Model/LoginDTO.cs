using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class LoginDTO
    {
        public string Policy { get; set; }
        [Required]
        public String Username { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
