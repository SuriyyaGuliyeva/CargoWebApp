using Cargo.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);      
    }
}
