using AdminPanelCargoWebApp.Models;
using AdminPanelCargoWebApp.ViewModels.Country;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminPanelCargoWebApp.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var countries = _countryRepository.GetAll();

            var models = new CountryViewModel();

            foreach (var country in countries)
            {
                var model = new CountryModel
                {
                    Name = country.Name,
                    CreationDateTime = country.CreationDateTime
                };

                if (models.Countries is null)
                {
                    models.Countries = new List<CountryModel>() { model };
                }
                else
                {
                    models.Countries.Add(model);
                }
            }

            return View(models);
        }
    }
}
