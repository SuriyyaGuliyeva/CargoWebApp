﻿using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.Constants;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;

namespace Cargo.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly ITotalCountService _totalCountService;
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(IShopService shopService, IUnitOfWork unitOfWork, ITotalCountService totalCountService)
        {
            _shopService = shopService;
            _unitOfWork = unitOfWork;           
            _totalCountService = totalCountService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var viewModel = new ShopViewModel();

            viewModel.Shops = _shopService.GetAll();           

            ViewBag.Message = Message;

            return View(viewModel);
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

            var countries = _unitOfWork.CountryRepository.GetAll();
            var categories = _unitOfWork.CategoryRepository.GetAll();

            var viewModel = new AddShopViewModel();

            viewModel.Shop = new AddShopModel();

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
            if (ModelState.IsValid == false)
                return View(viewModel);

            var model = viewModel.Shop;

            if (_shopService.IsExists(model.Name, model.SelectedCategory, model.SelectedCountry))
            {
                ViewBag.IsExistName = "This shop name already exists in this category!";

                return View(viewModel);
            }

            _shopService.Add(model);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int shopId)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var model = _shopService.GetUpdateModel(shopId);

            var viewModel = new UpdateShopViewModel()
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
            }

            foreach (var category in categories)
            {
                viewModel.CategoriesList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(UpdateShopViewModel viewModel)
        {
            if (ModelState.IsValid == false)
                return View(viewModel);

            var model = viewModel.Shop;

            if (_shopService.IsExists(model.Name, model.SelectedCategory, model.SelectedCountry))
            {
                ViewBag.IsExistName = "This shop belonging to this category is available in this country!";

                return View(viewModel);
            }

            _shopService.Update(model);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult UploadNewImage(int shopId)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var model = _shopService.GetUploadImageModel(shopId);

            var viewModel = new UploadImageShopViewModel()
            {
                Shop = model
            };           

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UploadNewImage(UploadImageShopViewModel viewModel)
        {
            if (ModelState.IsValid == false)
                return View(viewModel);

            var model = viewModel.Shop;          

            _shopService.UploadNewImage(model);

            Message = "Successfully Uploaded New Image!";

            return RedirectToAction(nameof(Index));            
        }

        [HttpPost]
        public IActionResult Delete(int shopId)
        {
            _shopService.Delete(shopId);

            Message = "Successfully Deleted!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DownloadImage(int shopId)
        {
            var model = _shopService.Get(shopId);

            string fullPath = Path.Combine(StorageConstants.ShopsPhotoDirectory, model.CoverPhotoUrl);

            var content = System.IO.File.ReadAllBytes(fullPath);

            return File(content, "img/jpg", $"{model.Name}.jpg");
        }
    }
}
