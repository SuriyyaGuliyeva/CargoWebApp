using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

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
                CountryId = model.SelectedCountry.Id,
                //CategoryId = int.Parse(selectedCategory) // TODO: use model.SelectedCategory.Id
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

            var country = _unitOfWork.CountryRepository.Get(shop.Country.Id);
            var category = _unitOfWork.CategoryRepository.Get(shop.Category.Id);

            ShopModel model = null;

            if (shop != null)
            {
                model = new ShopModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Link = shop.Link,
                    //SelectedCountry = country, // TODO: use countrymodel mapper
                    //CategoryName = category, // TODO: use categorymodel mapper
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
                var model = Map(shop);

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public ShopModel GetByCategoryId(string name, int categoryId)
        {
            var shop = _unitOfWork.ShopRepository.GetByCategoryId(name, categoryId);

            return Map(shop);
        }

        public void Update(ShopModel model)
        {
            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            string folder = "images/";
            folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;

            model.CoverPhotoUrl = "/" + folder;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            model.CoverPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Photo = model.CoverPhotoUrl,
                CountryId = model.SelectedCountry.Id,
                //CategoryId = int.Parse(selectedCategory) / TODO: use model.SelectedCategory.Id
            };

            _unitOfWork.ShopRepository.Update(shop);
        }
        
        private ShopModel Map(Shop shop)
        {
            if (shop == null)
                return null;

            var model = new ShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Link = shop.Link,
                //SelectedCountry = shop.Country, // TODO: use CountryMapper
                //SelectedCategory = shop.Category, // TODO: use CategoryMapper
                CoverPhotoUrl = shop.Photo,
                CreationDateTime = shop.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            return model;
        }
    }
}
