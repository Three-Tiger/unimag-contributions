using UniMagContributions.Dto.Auth;

namespace UniMagContributions.Services.Interface
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
    }
}
