using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(ShopModel model)
        {
            var selectedCountry = model.SelectedCountry;
            var selectedCategory = model.SelectedCategory;

            var shop = new Shop
            {
                Name = model.Name,
                Link = model.Link,
                Photo = "photo",
                CountryId = Int32.Parse(selectedCountry),
                CategoryId = Int32.Parse(selectedCategory)
            };

            _unitOfWork.ShopRepository.Add(shop);      
        }

        public void Delete(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            if (shop != null)
            {
                _unitOfWork.ShopRepository.Delete(id);
            }
        }

        public ShopModel Get(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            var countryName = _unitOfWork.CountryRepository.Get(shop.Country.Id).Name;
            var categoryName = _unitOfWork.CategoryRepository.Get(shop.Category.Id).Name;

            ShopModel model = null;

            if (shop != null)
            {
                model = new ShopModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Link = shop.Link,
                    CountryName = countryName,
                    CategoryName = categoryName,
                    Photo = shop.Photo,
                    CreationDateTime = shop.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)                   
                };
            }

            return model;
        }

        public IList<ShopModel> GetAll()
        {
            var shops = _unitOfWork.ShopRepository.GetAll();

            var viewModel = new ShopViewModel();

            viewModel.Shops = new List<ShopModel>();

            foreach (var shop in shops)
            {
                var countryName = _unitOfWork.CountryRepository.Get(shop.Country.Id).Name;
                var categoryName = _unitOfWork.CategoryRepository.Get(shop.Category.Id).Name;

                var model = new ShopModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Link = shop.Link,
                    CountryName = countryName,
                    CategoryName = categoryName,
                    Photo = shop.Photo,
                    CreationDateTime = shop.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public string GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ShopModel model)
        {
            var selectedCountry = model.SelectedCountry;
            var selectedCategory = model.SelectedCategory;

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Photo = "photo3",
                CountryId = Int32.Parse(selectedCountry),
                CategoryId = Int32.Parse(selectedCategory)
            };

            _unitOfWork.ShopRepository.Update(shop);
        }
    }
}
