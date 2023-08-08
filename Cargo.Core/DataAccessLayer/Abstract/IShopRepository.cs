using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IShopRepository : IGenericRepository<Shop>, ITotalCountRepository
    {
        Shop GetByCategoryId(string name, int categoryId, int countryId);
    }
}
