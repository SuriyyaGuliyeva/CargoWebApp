using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.AdminPanel.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly IShopService _shopService;

        public CategoryController(ICategoryService categoryService, ICountryService countryService, IShopService shopService)
        {
            _categoryService = categoryService;
            _countryService = countryService;
            _shopService = shopService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            int totalCountryCount = _countryService.GetTotalCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _categoryService.GetTotalCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _shopService.GetTotalShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            var viewModel = new CategoryViewModel();

            viewModel.Categories = _categoryService.GetAll();                       

            ViewBag.Message = Message;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int categoryId)
        {
            int totalCountryCount = _countryService.GetTotalCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _categoryService.GetTotalCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _shopService.GetTotalShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            var viewModel = _categoryService.Get(categoryId);         

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(AddCategoryViewModel viewModel)
        {
            var model = viewModel.Category;

            if (ModelState.IsValid == false)
                return View(viewModel);

            if (_categoryService.IsExists(model))
            {
                ViewBag.IsExistName = "This category name already exists!";
                return View(viewModel);
            }

            _categoryService.Update(viewModel);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int categoryId)
        {
            _categoryService.Delete(categoryId);

            Message = "Successfully Deleted!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            int totalCountryCount = _countryService.GetTotalCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _categoryService.GetTotalCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _shopService.GetTotalShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel viewModel)
        {
            var model = viewModel.Category;

            if (ModelState.IsValid == false)
                return View(viewModel);

            if (_categoryService.IsExists(model))
            {
                ViewBag.IsExistName = "This category name already exists!";

                return View(viewModel);
            }

            _categoryService.Add(viewModel);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }        
    }
}
