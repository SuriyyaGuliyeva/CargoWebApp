using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface ICountryRepository
    {
        void Add(Country country);
        IList<Country> GetAll();
        void Update(Country country);
        void Delete(int id);
        Country Get(int id);       
    }
}
