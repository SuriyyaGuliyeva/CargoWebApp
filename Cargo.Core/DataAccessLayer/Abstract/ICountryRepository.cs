using Cargo.Core.Domain.Entities;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Country GetByName(string name);
    }
}
