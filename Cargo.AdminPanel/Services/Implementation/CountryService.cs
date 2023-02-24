using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels.Country;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(CountryModel model)
        {
            var country = new Country
            {
                Name = model.Name
            };

            _unitOfWork.CountryRepository.Add(country);
        }

        public void Delete(int id)
        {
            var country = _unitOfWork.CountryRepository.Get(id);

            if (country != null)
            {
                _unitOfWork.CountryRepository.Delete(id);
            }
        }

        public CountryModel Get(int id)
        {            
            var country = _unitOfWork.CountryRepository.Get(id);

            CountryModel model = null;

            if (country != null)
            {
                model = new CountryModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    CreationDateTime = country.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };
            }            

            return model;
        }

        public IList<CountryModel> GetAll()
        {
            var countries = _unitOfWork.CountryRepository.GetAll();

            var viewModel = new CountryViewModel();

            viewModel.Countries = new List<CountryModel>();

            foreach (var country in countries)
            {
                var model = new CountryModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    CreationDateTime = country.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };

                viewModel.Countries.Add(model);
            }

            return viewModel.Countries;
        }

        public string GetByName(string name)
        {
            string addedCountryName = _unitOfWork.CountryRepository.GetByName(name);

            return addedCountryName;
        }

        public void Update(CountryModel model)
        {
            var country = new Country
            {
                Id = model.Id,
                Name = model.Name
            };

            _unitOfWork.CountryRepository.Update(country);
        }
    }
}
