using AutoMapper;
using UniMagContributions.Dto.User;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public void CreateRole(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleDto role)
        {
            throw new NotImplementedException();
        }

        public RoleDto GetRoleById(Guid id)
        {
            throw new NotImplementedException();
        }

        public RoleDto GetRoleByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<RoleDto> GetRoles()
        {
            List<Role> roles = _roleRepository.GetRoles();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public void UpdateRole(RoleDto role)
        {
            throw new NotImplementedException();
        }
    }
}
