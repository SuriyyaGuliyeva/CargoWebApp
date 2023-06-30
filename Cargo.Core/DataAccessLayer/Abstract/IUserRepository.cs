using Cargo.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<User> FindByNameAsync(string userId, CancellationToken cancellationToken);

        Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken);
        Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
        Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken);
        Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken);
    }
}
