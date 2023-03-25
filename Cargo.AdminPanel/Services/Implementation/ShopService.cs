using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Mappers.Abstract;
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
        private readonly IShopMapper _mapper;

        public ShopService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IShopMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public void Add(AddShopViewModel viewModel)
        {           
            var model = viewModel.Shop;

            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            //var base64String = string.Empty;

            //if (model.CoverPhoto != null)
            //{
            //    string folder = "images/";
            //    folder += model.CoverPhoto.FileName;

            //    model.CoverPhotoUrl = "/" + folder;               

            //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    var textBytes = Encoding.UTF8.GetBytes(model.CoverPhotoUrl);
            //    base64String = Convert.ToBase64String(textBytes);

            //    model.CoverPhotoUrl = base64String;

            //    model.CoverPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            //}

            //var shop = _mapper.Map(model);


            var shop = new Shop
            {
                Name = model.Name,
                Link = model.Link,
                //Photo = model.CoverPhotoUrl
                CountryId = model.SelectedCountry.Id,
                CategoryId = model.SelectedCategory.Id
            };

            _unitOfWork.ShopRepository.Add(shop);

            //var base64EncodedBytes = Convert.FromBase64String(base64String);
            //model.CoverPhotoUrl = Encoding.UTF8.GetString(base64EncodedBytes);
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
                model = _mapper.Map(shop, country, category);
            }

            return model;
        }

        public IList<ShopModel> GetAll()
        {
            var shops = _unitOfWork.ShopRepository.GetAllWithJoinQuery();

            var viewModel = new ShopViewModel();

            viewModel.Shops = new List<ShopModel>();

            foreach (var shop in shops)
            {
                var country = _unitOfWork.CountryRepository.Get(shop.Country.Id);
                var category = _unitOfWork.CategoryRepository.Get(shop.Category.Id);

                var model = _mapper.Map(shop, country, category);

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public void Update(ShopModel model)
        {
            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            //string folder = "images/";
            //folder += Guid.NewGuid().ToString() + "_" + model.CoverPhoto.FileName;

            //model.CoverPhotoUrl = "/" + folder;

            //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //model.CoverPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));

            //var shop = _mapper.Map(model);

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                //Photo = model.CoverPhotoUrl,
                CountryId = model.SelectedCountry.Id,
                CategoryId = model.SelectedCategory.Id
            };

            _unitOfWork.ShopRepository.Update(shop);
        }       

        public bool IsExists(ShopModel model)
        {
            var shopname = _unitOfWork.ShopRepository.GetByCategoryId(model.Name, model.SelectedCategory.Id);

            if (shopname == null)
            {
                return false;
            }

            return true;
        }       
    }
}
