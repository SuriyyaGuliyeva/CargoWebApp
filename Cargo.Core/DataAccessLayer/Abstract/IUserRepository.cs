using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindByName(string normalizedUserName);
        int GetTotalCount();
    }
}
