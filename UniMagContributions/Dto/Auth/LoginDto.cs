using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
