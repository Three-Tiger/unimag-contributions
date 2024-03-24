using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.User;

namespace UniMagContributions.Services.Interface
{
    public interface IUserService
    {
        FileContentResult GetUserProfile(Guid id);
        UserDto GetUserById(Guid userId);
        UserDto UpdateProfile(Guid id, UpdateUserDto updateUserDto);
        UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto);
        UserDto RemoveProfilePicture(Guid id);
        string DeleteUser(Guid userId);
        UserDto CreateUser(CreateUserDto userDto);
        List<UserDto> GetAllUser();
        string ChangePassword(Guid userId, ChangePasswordDto changePasswordDto);
    }
}
