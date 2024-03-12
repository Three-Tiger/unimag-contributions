using UniMagContributions.Constraints;
using UniMagContributions.Dto.User;

namespace UniMagContributions.Dto.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public UserDto User { get; set; }
    }
}
