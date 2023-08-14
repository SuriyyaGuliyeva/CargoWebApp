using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.AdminPanel.ViewModels.Country;
using Cargo.Core.DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, ICountryMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(AddCountryViewModel viewModel)
        {
            var model = viewModel.Country;
            var country = _mapper.Map(model);

            _unitOfWork.CountryRepository.Add(country);
        }

        public void Delete(int id)
        {
            _unitOfWork.CountryRepository.Get(id);           

            _unitOfWork.CountryRepository.Delete(id);
        }

        public AddCountryViewModel Get(int id)
        {            
            var country = _unitOfWork.CountryRepository.Get(id);

            var model = _mapper.Map(country);

            var viewModel = new AddCountryViewModel
            {
                Country = model
            };

            return viewModel;
        }

        public IList<CountryModel> GetAll()
        {
            var countries = _unitOfWork.CountryRepository.GetAll();

            var viewModel = new CountryViewModel
            {
                Countries = new List<CountryModel>()
            };

            foreach (var country in countries)
            {
                var model = _mapper.Map(country);

                viewModel.Countries.Add(model);
            }

            return viewModel.Countries;
        }

        public void Update(AddCountryViewModel viewModel)
        {
            var model = viewModel.Country;
            var country = _mapper.Map(model);

            _unitOfWork.CountryRepository.Update(country);
        }

        public bool IsExists(CountryModel model)
        {
            var countryName = _unitOfWork.CountryRepository.GetByName(model.Name);

            if (countryName == null)
            {
                return false;
            }

            return true;
        }

        //public int GetTotalCountryCount()
        //{
        //    return _unitOfWork.CountryRepository.GetTotalCount();
        //}
    }
}
