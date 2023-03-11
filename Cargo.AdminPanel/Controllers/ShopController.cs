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

            model.CountriesSelectList = new List<SelectListItem>();
            model.CategoriesSelectList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.CountriesSelectList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                model.CategoriesSelectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };

            return View(model);
        }   

        [HttpPost]
        public IActionResult Add(ShopModel model)
        {
            _shopService.Add(model);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int shopId)
        {
            var shop = _shopService.Get(shopId);

            var model = new ShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Link = shop.Link,
                Photo = shop.Photo,
                CountryName = shop.CountryName
            };

            var countries = _unitOfWork.CountryRepository.GetAll();
            var categories = _unitOfWork.CategoryRepository.GetAll();           

            model.CountriesSelectList = new List<SelectListItem>();
            model.CategoriesSelectList = new List<SelectListItem>();

            foreach (var country in countries)
            {
                model.CountriesSelectList.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            };

            foreach (var category in categories)
            {
                model.CategoriesSelectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
            };

            Message = "Successfully Updated!";

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ShopModel model)
        {
            _shopService.Update(model);

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
