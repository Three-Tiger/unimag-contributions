using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.Auth;
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
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, IFileService fileService, IConfiguration configuration)

        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _fileService = fileService;
            _configuration = configuration;
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
                var result = _fileService.SaveImage(registerDto.ProfilePicture);
                // if it is not right format
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = _fileService.getFilePath(result.Item2);
            }

            // Create user
            _userRepository.CreateUser(user);

            return "Register success!";
        }

        public string Login(LoginDto loginDto)
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
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            TimeSpan tokenLifeTime = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:TokenLifeTime"]));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new("userId", user.UserId.ToString()),
                new(ClaimTypes.Role, user.Role.Name),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(tokenLifeTime),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
