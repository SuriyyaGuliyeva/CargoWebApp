using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface ICountryService
    {
        void Add(AddCountryViewModel model);
        void Update(AddCountryViewModel model);
        void Delete(int id);
        IList<CountryModel> GetAll();
        AddCountryViewModel Get(int id);
        bool IsExists(CountryModel model);
        int GetTotalCount();
    }
}
