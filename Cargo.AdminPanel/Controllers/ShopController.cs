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

            if (ModelState.IsValid == false)
                return View(model);

            if (IsExists(model))
            {
                ViewBag.IsExistName = "This shop name already exists in this category!";

                return View(model);
            }

            _shopService.Add(model);

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

            if (ModelState.IsValid == false)
                return View(model);

            if (IsExists(model))
            {
                ViewBag.IsExistName = "This shop name already exists in this category!";

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
            return true;
            //var shop = _shopService.GetByCategoryId(model.Name, int.Parse(model.SelectedCategory));

            //return shop != null;
        }
    }
}
