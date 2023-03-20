using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IShopRepository : IGenericRepository<Shop>
    {
        int GetByCategoryId(string name, int categoryId);
    }
}
