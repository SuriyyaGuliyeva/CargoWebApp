using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {          
            var viewModel = new CountryViewModel();
            viewModel.Countries = _countryService.GetAll();

            ViewBag.Message = Message;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int countryId)
        {
            var country = _countryService.Get(countryId);

            var model = new CountryModel
            {
                Id = country.Id,
                Name = country.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CountryModel model)
        {
            if (!ModelState.IsValid || !IsExists(model))
            {
                return View(model);
            }

            _countryService.Update(model);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int countryId)
        {
            _countryService.Delete(countryId);

            Message = "Successfully Deleted!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CountryModel model)
        {
            if (!ModelState.IsValid || !IsExists(model))
            {
                return View(model);
            }

            _countryService.Add(model);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        public bool IsExists(CountryModel model)
        {
            string addedCountryName = _countryService.GetByName(model.Name);

            if (model.Name.Equals(addedCountryName))
            {
                ViewBag.IsExistName = "This Country Name already exists!";
                return false;
            }

            return true;
        }
    }
}
