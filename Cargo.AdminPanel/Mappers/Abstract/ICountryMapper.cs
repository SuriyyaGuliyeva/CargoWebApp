using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface ICountryMapper
    {
        CountryModel Map(Country country);
        Country Map(CountryModel model);       
    }
}
