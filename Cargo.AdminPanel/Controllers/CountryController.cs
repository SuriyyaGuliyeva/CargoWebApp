using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
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
            var viewModel = _countryService.Get(countryId);         

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(AddCountryViewModel viewModel)
        {
            var model = viewModel.Country;

            if (ModelState.IsValid == false)
                return View(viewModel);

            if (_countryService.IsExists(model))
            {
                ViewBag.IsExistName = "This country name already exists!";
                return View(viewModel);
            }

            _countryService.Update(viewModel);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
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
        public IActionResult Add(AddCountryViewModel viewModel)
        {
            var model = viewModel.Country;

            if (ModelState.IsValid == false)
                return View(viewModel);

            if (_countryService.IsExists(model))
            {
                ViewBag.IsExistName = "This country name already exists!";

                return View(viewModel);
            }

            _countryService.Add(viewModel);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }
    }
}
