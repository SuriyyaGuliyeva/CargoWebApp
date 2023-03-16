using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;        

        public ShopService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Add(ShopModel model)
        {            
            var selectedCountry = model.SelectedCountry;
            var selectedCategory = model.SelectedCategory;

            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            if (model.CoverPhoto != null)
            {
                string folder = "images/";
                folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;

                model.CoverPhotoUrl = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                model.CoverPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            var shop = new Shop
            {
                Name = model.Name,
                Link = model.Link,
                Photo = model.CoverPhotoUrl,
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
                    CoverPhotoUrl = shop.Photo,
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
                var countryName = string.Empty;

                var country = _unitOfWork.CountryRepository.Get(shop.Country.Id);

                if (country != null)
                {
                    countryName = country.Name;
                }

                var categoryName = string.Empty;

                var category = _unitOfWork.CategoryRepository.Get(shop.Category.Id);

                if (category != null)
                {
                    categoryName = category.Name;
                }

                var model = new ShopModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Link = shop.Link,
                    CountryName = countryName,
                    CategoryName = categoryName,
                    CoverPhotoUrl = shop.Photo,
                    CreationDateTime = shop.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
                };

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public string GetByName(string name)
        {
            string addedShopName = _unitOfWork.ShopRepository.GetByName(name);

            return addedShopName;
        }

        public int GetByCategoryId(string name)
        {
            int addedCategoryId = _unitOfWork.ShopRepository.GetByCategoryId(name);

            return addedCategoryId;
        }

        public void Update(ShopModel model)
        {
            var selectedCountry = model.SelectedCountry;
            var selectedCategory = model.SelectedCategory;

            if (model.Link == null)
            {
                model.Link = string.Empty;
            }            

            if (model.CoverPhoto != null)
            {
                string folder = "images/";
                folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;

                model.CoverPhotoUrl = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                model.CoverPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Photo = model.CoverPhotoUrl,
                CountryId = Int32.Parse(selectedCountry),
                CategoryId = Int32.Parse(selectedCategory)
            };

            _unitOfWork.ShopRepository.Update(shop);
        }               
    }
}
