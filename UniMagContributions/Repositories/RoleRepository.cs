using Microsoft.EntityFrameworkCore;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error creating role");
            }
        }

        public Role GetRoleByName(string name)
        {
            try
            {
                return _context.Roles.FirstOrDefault(r => r.Name == name);
            }
            catch (Exception)
            {
                throw new Exception("Error getting role");
            }
        }

        public Role GetRoleById(Guid id)
        {
            try
            {
                return _context.Roles.FirstOrDefault(r => r.RoleId == id);
            }
            catch (Exception)
            {
                throw new Exception("Error getting role");
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                _context.Entry<Role>(role).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error updating role");
            }
        }

        public void DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting role");
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                return _context.Roles
                    .Where(r => r.Name != "Administrator")
                    .ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error getting roles");
            }
        }
    }
}
