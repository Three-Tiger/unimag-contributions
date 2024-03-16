using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.User;

namespace UniMagContributions.Services.Interface
{
    public interface IUserService
    {
        FileContentResult GetUserProfile(Guid id);
        UserDto GetUserById(Guid userId);
        UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto);
        string DeleteUser(Guid userId);
        UserDto CreateUser(UserDto userDto);
        List<UserDto> GetAllUser();
        string ChangePassword(Guid userId, ChangePasswordDto changePasswordDto);
    }
}
