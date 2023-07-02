using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Role FindByName(string normalizedRoleName);
    }
}
