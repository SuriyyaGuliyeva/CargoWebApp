using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);
        Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);      
        Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);
        void Dispose();
    }
}
