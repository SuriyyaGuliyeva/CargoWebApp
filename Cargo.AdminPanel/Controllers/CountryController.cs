using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.AdminPanel.ViewModels.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ITotalCountService _totalCountService;

        public CountryController(ICountryService countryService, ITotalCountService totalCountService)
        {
            _countryService = countryService;           
            _totalCountService = totalCountService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new CountryViewModel();

            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            viewModel.Countries = _countryService.GetAll();            
         
            ViewBag.Message = Message;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int countryId)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

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
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

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
