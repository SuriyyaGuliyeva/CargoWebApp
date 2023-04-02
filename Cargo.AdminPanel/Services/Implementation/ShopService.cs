using Cargo.AdminPanel.Helpers;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Hosting;
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

            var hashCodeImage = string.Empty;

            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            if (model.CoverPhoto != null)
            {
                string folder = "images\\";
                folder += model.CoverPhoto.FileName;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                var fileStream = new FileStream(serverFolder, FileMode.Create);

                model.CoverPhoto.CopyTo(fileStream);

                fileStream.Close();

                hashCodeImage = GetHashCodeImage.GetImageHashCode(serverFolder);

                model.CoverPhotoUrl = "\\" + folder;
            }

            var shop = _mapper.Map(model, hashCodeImage);

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
                model = _mapper.Map(shop, country, category);
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
                var country = _unitOfWork.CountryRepository.Get(shop.Country.Id);
                var category = _unitOfWork.CategoryRepository.Get(shop.Category.Id);

                var model = _mapper.Map(shop, country, category);

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public void Update(ShopModel model)
        {
            var hashCodeImage = string.Empty;
            string folder = string.Empty;
            string serverFolder = string.Empty;

            if (model.Link == null)
            {
                model.Link = string.Empty;
            }

            if (model.CoverPhoto == null)
            {
                folder = model.CoverPhotoUrl;
                folder = folder.Substring(1);

                serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);                              
            }
            else
            {
                folder = "images\\";
                folder += model.CoverPhoto.FileName;

                serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                var fileStream = new FileStream(serverFolder, FileMode.Create);

                model.CoverPhoto.CopyTo(fileStream);

                fileStream.Close();               
            }

            hashCodeImage = GetHashCodeImage.GetImageHashCode(serverFolder);

            model.CoverPhotoUrl = "\\" + folder;

            var shop = _mapper.Map(model, hashCodeImage);

            _unitOfWork.ShopRepository.Update(shop);
        }

        public bool IsExists(ShopModel model, CategoryModel selectedCategory, CountryModel selectedCountry)
        {
            var shopname = _unitOfWork.ShopRepository.GetByCategoryId(model.Name, selectedCategory.Id, selectedCountry.Id);

            if (shopname == null)
            {
                return false;
            }

            return true;
        }
    }
}
