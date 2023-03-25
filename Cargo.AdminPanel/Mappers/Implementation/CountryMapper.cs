using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.Domain.Entities;
using System;

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
