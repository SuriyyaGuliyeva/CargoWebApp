using Cargo.AdminPanel.Models;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface ICountryService
    {
        void Add(CountryModel model);
        void Update(CountryModel model);
        void Delete(int id);
        IList<CountryModel> GetAll();
        CountryModel Get(int id);
        string GetByName(string name);
    }
}
