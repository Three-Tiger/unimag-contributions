using System.Threading.Tasks;
using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
    public interface IUserRepository
    {
        void CreateUser(User user);
		Task<User> GetUserByEmailAsync(string email);
        User GetUserById(Guid id);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
