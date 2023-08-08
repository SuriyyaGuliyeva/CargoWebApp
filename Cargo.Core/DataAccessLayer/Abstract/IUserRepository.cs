using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository : IGenericRepository<User>, ITotalCountRepository
    {
        User FindByName(string normalizedUserName);
    }
}
