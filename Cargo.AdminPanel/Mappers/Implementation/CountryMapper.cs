using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Constants;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class CountryMapper : ICountryMapper
    {
        public CountryModel Map(Country country)
        {
            if (country == null)
                return null;

            var model = new CountryModel
            {
                Id = country.Id,
                Name = country.Name,
                CreationDateTime = country.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            return model;
        }

        public Country Map(CountryModel model)
        {
            if (model == null)
                return null;

            var country = new Country
            {
                Id = model.Id,
                Name = model.Name
            };

            return country;
        }
    }
}
