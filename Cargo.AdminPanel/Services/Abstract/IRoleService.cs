using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateAsync(RoleModel model);

        Task<IdentityResult> DeleteAsync(RoleModel model);

        Task<RoleModel> FindByIdAsync(string roleId);

        Task<RoleModel> FindByNameAsync(string normalizedRoleName);

        Task<string> GetNormalizedRoleNameAsync(RoleModel model);

        Task<string> GetRoleIdAsync(RoleModel model);

        Task<string> GetRoleNameAsync(RoleModel model);

        Task SetNormalizedRoleNameAsync(RoleModel model, string normalizedName);

        Task SetRoleNameAsync(RoleModel model, string roleName);

        Task<IdentityResult> UpdateAsync(RoleModel model);

        void Dispose();
    }
}
