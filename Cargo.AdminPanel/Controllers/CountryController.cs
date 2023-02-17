using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels.Country;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cargo.AdminPanel.Controllers
{
    public class CountryController : Controller
    {
        private const string _dateTimeFormat = "dd.MM.yy HH:mm:ss";

        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index()
        {
            var countries = _unitOfWork.CountryRepository.GetAll();

            var models = new CountryViewModel();            

            if (countries.Count != 0)
            {
                foreach (var country in countries)
                {
                    var model = new CountryModel
                    {
                        Id = country.Id,
                        Name = country.Name,
                        CreationDateTime = country.CreationDateTime.ToString(_dateTimeFormat)
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
            }

            ViewBag.Message = Message;

            return View(models);            
        }

        [HttpGet]
        public IActionResult Update(int countryId)
        {
            var country = _unitOfWork.CountryRepository.Get(countryId);

            var model = new CountryModel
            {
                Id = country.Id,
                Name = country.Name,
                CreationDateTime = country.CreationDateTime.ToString(_dateTimeFormat)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CountryModel model)
        {
            var country = new Country
            {
                Id = model.Id,
                Name = model.Name,
                CreationDateTime = DateTime.ParseExact(model.CreationDateTime, _dateTimeFormat, CultureInfo.InvariantCulture)
            };

            _unitOfWork.CountryRepository.Update(country);

            Message = "Successfully Updated!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int countryId)
        {
            var country = _unitOfWork.CountryRepository.Get(countryId);

            if (country != null)
            {
                _unitOfWork.CountryRepository.Delete(countryId);
            }

            Message = "Successfully Deleted!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CountryModel model)
        {
            var country = new Country
            {
                Name = model.Name,
                CreationDateTime = DateTime.Now
            };

            _unitOfWork.CountryRepository.Add(country);

            Message = "Successfully Added!";

            return RedirectToAction("Index");
        }
    }
}
