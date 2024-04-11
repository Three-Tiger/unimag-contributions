using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment environment;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IFeedbackService _feedbackService;
        private readonly IContributionService _contributionService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IWebHostEnvironment env, IMapper mapper, IFileService fileService, IFeedbackService feedbackService, IContributionService contributionService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _feedbackService = feedbackService;
            _contributionService = contributionService;
            environment = env;
            _mapper = mapper;
        }

        public UserDto CreateUser(CreateUserDto userDto)
        {
            // Get user by email
            User user = _userRepository.GetUserByEmail(userDto.Email);
            if (user != null)
            {
                throw new ConflictException("Email already exists");
            }

            // Hash password
            var passwordHasher = new PasswordHasher<string>();
            userDto.Password = passwordHasher.HashPassword(null, userDto.Password);

            // Map registerDto to user
            user = _mapper.Map<User>(userDto);

            // Save profile picture
            if (userDto.NewProfilePicture != null)
            {
                var result = _fileService.SaveFile(userDto.NewProfilePicture, EFolder.ProfilePicture);
                // if it is not right format
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = result.Item2;
            }

            // Create user
            _userRepository.CreateUser(user);

            return _mapper.Map<UserDto>(_userRepository.GetUserById(user.UserId));
        }

        public string DeleteUser(Guid userId)
        {
            User user = _userRepository.GetUserById(userId) ?? throw new NotFoundException("User not found");

            if (user.ProfilePicture != null)
            {
                _fileService.DeleteFile(user.ProfilePicture);
            }

            foreach (var item in user.Feedbacks)
            {
                _feedbackService.DeleteFeedback(item.FeedBackId);
            }

            foreach (var item in user.Contributions)
            {
                _contributionService.DeleteContribution(item.ContributionId);
            }

            _userRepository.DeleteUser(user);

            return "User deleted successfully";
        }

        public List<UserDto> GetAllUser()
        {
            List<User> users = _userRepository.GetAllUser();
            return _mapper.Map<List<UserDto>>(users);
        }

        public UserDto GetUserById(Guid userId)
        {
            User user = _userRepository.GetUserById(userId) ?? throw new NotFoundException("User not found");
            return _mapper.Map<UserDto>(user);
        }

        public FileContentResult GetUserProfile(Guid id)
        {
            User user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");

            if (string.IsNullOrEmpty(user.ProfilePicture))
            {
                throw new NotFoundException("User does not have profile image");
            }
            return _fileService.GetFile(user.ProfilePicture);
        }

        public UserDto UpdateProfile(Guid id, UpdateUserDto updateUserDto)
        {
            User user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");

            updateUserDto.UserId = id;
            updateUserDto.Password ??= user.Password;
            updateUserDto.RoleId ??= user.RoleId;

            user = _mapper.Map<User>(updateUserDto);

            if (updateUserDto.NewProfilePicture != null)
            {
                var result = _fileService.SaveFile(updateUserDto.NewProfilePicture, EFolder.ProfilePicture);
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = result.Item2;
                _fileService.DeleteFile(updateUserDto.ProfilePicture);
            }

            if (updateUserDto.ProfilePicture == "null")
            {
                user.ProfilePicture = null;
            }

            _userRepository.UpdateUser(user);

            return _mapper.Map<UserDto>(user);
        }

        public UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            User user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
            updateUserDto.UserId = id;

            if (updateUserDto.Password != null)
            {
                updateUserDto.Password = HashPassword(updateUserDto.Password);
            } else
            {
                updateUserDto.Password = user.Password;
            }

            user = _mapper.Map<User>(updateUserDto);

            if (updateUserDto.NewProfilePicture != null)
            {
                var result = _fileService.SaveFile(updateUserDto.NewProfilePicture, EFolder.ProfilePicture);
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = result.Item2;
                _fileService.DeleteFile(updateUserDto.ProfilePicture);
            }

            if (updateUserDto.ProfilePicture == "null")
            {
                user.ProfilePicture = null;
            }

            _userRepository.UpdateUser(user);

            return _mapper.Map<UserDto>(_userRepository.GetUserById(user.UserId));
        }

        public UserDto RemoveProfilePicture(Guid id)
        {
            User user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
            _fileService.DeleteFile(user.ProfilePicture);
            user.ProfilePicture = null;
            _userRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(_userRepository.GetUserById(user.UserId));
        }

        public string ChangePassword(Guid userId, ChangePasswordDto changePasswordDto)
        {
            User user = _userRepository.GetUserById(userId) ?? throw new NotFoundException("User not found");

            if (!VerifyPassword(user.Password, changePasswordDto.CurrentPassword))
            {
                throw new InvalidException("Current password is incorrect");
            }

            user.Password = HashPassword(changePasswordDto.NewPassword);
            _userRepository.UpdateUser(user);

            return "Password changed successfully";
        }

        public bool VerifyPassword(string currentPassword, string oldPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, currentPassword, oldPassword) == PasswordVerificationResult.Failed)
            {
                return false;
            }
            return true;
        }

        public string HashPassword(string newPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(null, newPassword);
        }
    }
}
