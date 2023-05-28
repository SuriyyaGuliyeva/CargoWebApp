using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleMapper _roleMapper;

        public RoleService(IUnitOfWork unitOfWork, IRoleMapper roleMapper)
        {
            _unitOfWork = unitOfWork;
            _roleMapper = roleMapper;
        }

        public Task<IdentityResult> CreateAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            _unitOfWork.RoleRepository.CreateAsync(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            _unitOfWork.RoleRepository.DeleteAsync(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.RoleRepository.Dispose();
        }

        public Task<RoleModel> FindByIdAsync(string roleId)
        {
            var role = _unitOfWork.RoleRepository.FindByIdAsync(roleId).Result;

            var model = _roleMapper.Map(role);

            return Task.FromResult(model);
        }

        public Task<RoleModel> FindByNameAsync(string normalizedRoleName)
        {
            var role = _unitOfWork.RoleRepository.FindByNameAsync(normalizedRoleName).Result;

            var model = _roleMapper.Map(role);

            return Task.FromResult(model);
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            var normalizedUserName = _unitOfWork.RoleRepository.GetNormalizedRoleNameAsync(role);

            return normalizedUserName;
        }

        public Task<string> GetRoleIdAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            var roleId = _unitOfWork.RoleRepository.GetRoleIdAsync(role);

            return roleId;
        }

        public Task<string> GetRoleNameAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            var rolename = _unitOfWork.RoleRepository.GetRoleNameAsync(role);

            return rolename;
        }

        public Task SetNormalizedRoleNameAsync(RoleModel model, string normalizedName)
        {
            var role = _roleMapper.Map(model);

            _unitOfWork.RoleRepository.SetNormalizedRoleNameAsync(role, normalizedName);

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(RoleModel model, string roleName)
        {
            var role = _roleMapper.Map(model);

            _unitOfWork.RoleRepository.SetRoleNameAsync(role, roleName);

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(RoleModel model)
        {
            var role = _roleMapper.Map(model);

            _unitOfWork.RoleRepository.UpdateAsync(role);

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
