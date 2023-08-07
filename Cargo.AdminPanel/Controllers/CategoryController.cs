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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new CategoryViewModel();

            viewModel.Categories = _categoryService.GetAll();

            ViewBag.Message = Message;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int categoryId)
        {
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
