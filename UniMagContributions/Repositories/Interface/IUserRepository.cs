using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        Task<User> GetUserByUsernameAsync(string usernamme);
        User GetUserByEmail(string email);
        User GetUserById(Guid id);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
