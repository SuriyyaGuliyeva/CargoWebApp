using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(IShopService shopService, IUnitOfWork unitOfWork)
        {
            _shopService = shopService;
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ShopViewModel();

            viewModel.Shops = _shopService.GetAll();

            ViewBag.Message = Message;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var countries = _unitOfWork.CountryRepository.GetAll();
            var categories = _unitOfWork.CategoryRepository.GetAll();

            var viewModel = new AddShopViewModel();

            viewModel.Shop = new ShopModel();
            viewModel.CountriesList = new List<SelectListItem>();
            viewModel.CategoriesList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                viewModel.CountriesList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                viewModel.CategoriesList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };     

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddShopViewModel viewModel)
        {
            var model = viewModel.Shop;            

            // to get selected value from Dropdown List Country - START            
            var selectedCountryValue = Request.Form["selectedCountry"].ToString();

            if (selectedCountryValue.Equals(string.Empty))
            {
                ViewBag.CheckEmptyOrNot = "Please enter a country name!";

                return View(viewModel);
            }
            else
            {
                var country = _unitOfWork.CountryRepository.Get(Int32.Parse(selectedCountryValue));

                var countryModel = new CountryModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    CreationDateTime = country.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };

                model.SelectedCountry = countryModel;
            }       
            // to get selected value from Dropdown List Country - END

            // to get selected value from Dropdown List Category - START
            var selectedCategoryValue = Request.Form["selectedCategory"].ToString();

            if (selectedCategoryValue.Equals(string.Empty))
            {
                //ViewBag.CheckEmptyOrNot = "Please enter a category name!";

                //return View(viewModel);
            }
            else
            {
                var category = _unitOfWork.CategoryRepository.Get(Int32.Parse(selectedCategoryValue));

                var categoryModel = new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreationDateTime = category.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };

                model.SelectedCategory = categoryModel;
            }
            // to get selected value from Dropdown List Category - END
            
            if (!ModelState.IsValid)
                return View(viewModel);

            if (_shopService.IsExists(model, model.SelectedCategory, model.SelectedCountry))
            {
                ViewBag.IsExistName = "This shop name already exists in this category!";

                return View(viewModel);
            }

            _shopService.Add(viewModel);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int shopId)
        {
            var model = _shopService.Get(shopId);

            var viewModel = new AddShopViewModel()
            {
                Shop = model
            };

            var countries = _unitOfWork.CountryRepository.GetAll();
            var categories = _unitOfWork.CategoryRepository.GetAll();

            viewModel.CountriesList = new List<SelectListItem>();
            viewModel.CategoriesList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                viewModel.CountriesList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                viewModel.CategoriesList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(AddShopViewModel viewModel)
        {
            var model = viewModel.Shop;

            // to get selected value from Dropdown List Country - START
            var selectedCountryValue = Request.Form["selectedCountry"].ToString();

            var country = _unitOfWork.CountryRepository.Get(Int32.Parse(selectedCountryValue));

            var countryModel = new CountryModel
            {
                Id = country.Id,
                Name = country.Name,
                CreationDateTime = country.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            model.SelectedCountry = countryModel;
            // to get selected value from Dropdown List Country - END

            // to get selected value from Dropdown List Category - START
            var selectedCategoryValue = Request.Form["selectedCategory"].ToString();

            var category = _unitOfWork.CategoryRepository.Get(Int32.Parse(selectedCategoryValue));

            var categoryModel = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                CreationDateTime = category.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            model.SelectedCategory = categoryModel;
            // to get selected value from Dropdown List Category - END

            if (ModelState.IsValid == false)
                return View(viewModel);

            if (_shopService.IsExists(model, model.SelectedCategory, model.SelectedCountry))
            {
                ViewBag.IsExistName = "This shop belonging to this category is available in this country!";

                return View(viewModel);
            }

            _shopService.Update(model);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int shopId)
        {
            _shopService.Delete(shopId);

            Message = "Successfully Deleted!";

            return RedirectToAction(nameof(Index));
        }
    }
}
