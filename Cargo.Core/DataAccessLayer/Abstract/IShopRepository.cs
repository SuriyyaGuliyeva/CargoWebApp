using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IShopRepository : IGenericRepository<Shop>
    {
        Shop GetByCategoryId(string name, int categoryId);
    }
}
