using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface ICountryRepository : IGenericRepository<Country>, ITotalCountRepository
    {
        Country GetByName(string name);
    }
}
