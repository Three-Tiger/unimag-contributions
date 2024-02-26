using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
    public interface IRoleRepository
    {
        void CreateRole(Role role);
        Role GetRoleByName(string name);
        Role GetRoleById(Guid id);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
    }
}
