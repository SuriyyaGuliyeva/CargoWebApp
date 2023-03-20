using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
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

            var model = new ShopModel();

            model.CountriesList = new List<SelectListItem>();
            model.CategoriesList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.CountriesList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                model.CategoriesList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ShopModel model)
        {
            if (!ModelState.IsValid || !IsExists(model))
            {
                return View(model);
            }

            _shopService.Add(model);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int shopId)
        {
            var currentShopModel = _shopService.Get(shopId);

            var model = new ShopModel
            {
                Id = currentShopModel.Id,
                Name = currentShopModel.Name,
                Link = currentShopModel.Link,
                CoverPhoto = currentShopModel.CoverPhoto,
                CoverPhotoUrl = currentShopModel.CoverPhotoUrl,
                CountryName = currentShopModel.CountryName,
                CategoryName = currentShopModel.CategoryName
            };

            var countries = _unitOfWork.CountryRepository.GetAll();
            var categories = _unitOfWork.CategoryRepository.GetAll();

            model.CountriesList = new List<SelectListItem>();
            model.CategoriesList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.CountriesList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                model.CategoriesList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ShopModel model)
        {
            if (!ModelState.IsValid || !IsExists(model))
            {
                return View(model);
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

        public bool IsExists(ShopModel model)
        {
            string addedShopName = _shopService.GetByName(model.Name);
            int addedCategoryId = _shopService.GetByCategoryId(model.Name, Int32.Parse(model.SelectedCategory));

            if (model.Name.Equals(addedShopName) && model.SelectedCategory.Equals(addedCategoryId.ToString()))
            {
                ViewBag.IsExistName = "This shop name already exists in this category!";
                return false;
            }

            return true;
        }
    }
}
