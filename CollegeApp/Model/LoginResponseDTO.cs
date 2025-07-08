using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class LoginResponseDTO
    {
        [Required]
        public String Username { get; set; }
        [Required]
        public String Token { get; set; }
    }
}
