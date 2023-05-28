using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreateAsync(Role role);

        Task<IdentityResult> DeleteAsync(Role role);

        Task<Role> FindByIdAsync(string roleId);

        Task<Role> FindByNameAsync(string normalizedRoleName);

        Task<string> GetNormalizedRoleNameAsync(Role role);

        Task<string> GetRoleIdAsync(Role role);

        Task<string> GetRoleNameAsync(Role role);

        Task SetNormalizedRoleNameAsync(Role role, string normalizedName);

        Task SetRoleNameAsync(Role role, string roleName);

        Task<IdentityResult> UpdateAsync(Role role);

        void Dispose();
    }
}
