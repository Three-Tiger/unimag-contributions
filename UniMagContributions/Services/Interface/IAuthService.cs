using UniMagContributions.Dto.Auth;

namespace UniMagContributions.Services.Interface
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);
        string Login(LoginDto loginDto);
    }
}
