using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.Auth;
using UniMagContributions.Dto.User;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IFileService _fileService;

        public AuthService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, IFileService fileService)

        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _fileService = fileService;
        }

        public string Register(RegisterDto registerDto)
        {
            // Get user by email
            User user = _userRepository.GetUserByEmail(registerDto.Email);
            if (user != null)
            {
                throw new ConflictException("Email already exists");
            }

            // Hash password
            var passwordHasher = new PasswordHasher<string>();
            registerDto.Password = passwordHasher.HashPassword(null, registerDto.Password);

            // Map registerDto to user
            user = _mapper.Map<User>(registerDto);

            // Get role by name
            Role role = _roleRepository.GetRoleByName(ERole.Student.ToString()) ?? throw new Exception("Role not found");
            user.RoleId = role.RoleId;

            // Save profile picture
            if (registerDto.ProfilePicture != null)
            {
                var result = _fileService.SaveFile(registerDto.ProfilePicture, EFolder.ProfilePicture);
                // if it is not right format
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = result.Item2;
            }

            // Create user
            _userRepository.CreateUser(user);

            return "Register success!";
        }

        public AuthResponse Login(LoginDto loginDto)
        {
            // Get user by email
            User user = _userRepository.GetUserByEmail(loginDto.Email) ?? throw new AuthenticationException("Invalid Credentials!");

            // Verify password
            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, user.Password, loginDto.Password) == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Invalid Credentials!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));
            TimeSpan tokenLifeTime = TimeSpan.FromMinutes(Convert.ToDouble(Environment.GetEnvironmentVariable("JWT_TOKEN_LIFE_TIME")));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("JWT_SUBJECT")),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new("userId", user.UserId.ToString()),
                new(ClaimTypes.Role, user.Role.Name),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(tokenLifeTime),
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);
            string role = user.Role.Name;

            UserDto userDto = _mapper.Map<UserDto>(user);

            return new AuthResponse { AccessToken = accessToken, Role = role, User = userDto };
        }
    }
}
