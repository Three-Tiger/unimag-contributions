using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.User;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment environment;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IWebHostEnvironment env, IMapper mapper, IFileService fileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            environment = env;
            _mapper = mapper;
        }

        public UserDto CreateUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public string DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllUser()
        {
            throw new NotImplementedException();
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

            string wwwPath = this.environment.WebRootPath;
            var file = Path.Combine(wwwPath, user.ProfilePicture);

            if (!File.Exists(file))
            {
                throw new NotFoundException("File not found");
            }

            byte[] fileBytes = File.ReadAllBytes(file);
            return new FileContentResult(fileBytes, "image/jpeg");
        }

        public UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            User user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
            var oldProfilePicture = user.ProfilePicture;

            updateUserDto.UserId = id;
            updateUserDto.Password ??= user.Password;
            updateUserDto.RoleId ??= user.RoleId;

            user = _mapper.Map<User>(updateUserDto);
            user.ProfilePicture = oldProfilePicture;

            if (updateUserDto.ProfilePicture != null)
            {
                var result = _fileService.SaveFile(updateUserDto.ProfilePicture, EFolder.ProfilePicture);
                if (result.Item1 == 0)
                {
                    throw new InvalidException(result.Item2);
                }
                user.ProfilePicture = result.Item2;
                if (!_fileService.DeleteFile(oldProfilePicture))
                {
                    throw new Exception("Error deleting old profile picture");
                }
            }

            _userRepository.UpdateUser(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
