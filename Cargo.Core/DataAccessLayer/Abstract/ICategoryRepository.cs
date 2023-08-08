using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>, ITotalCountRepository
    {
        Category GetByName(string name);
    }
}
