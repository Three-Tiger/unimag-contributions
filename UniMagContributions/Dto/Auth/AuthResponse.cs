using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string Role { get; set; }
    }
}
