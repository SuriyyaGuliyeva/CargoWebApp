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
        private readonly IShopMapper _shopMapper;
        private readonly IAddShopMapper _addShopMapper;
        public ShopService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IShopMapper shopMapper, IAddShopMapper addShopMapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _shopMapper = shopMapper;
            _addShopMapper = addShopMapper;
        }

        public void Add(AddShopModel model)
        {
            var hashCodeImage = string.Empty;

            // 1. Get binary data for model.coverPhoto (may be we can use MemoryStream) 
            // 2. Calculate hash for binary data
            // 3. Save photo with hash name in specified folder 
            // 4. Set hash for shop.Photo

            //if (model.CoverPhoto != null)
            //{
            //    string folder = "images\\";
            //    folder += model.CoverPhoto.FileName;

            //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    var fileStream = new FileStream(serverFolder, FileMode.Create);

            //    model.CoverPhoto.CopyTo(fileStream);

            //    fileStream.Close();

            //    hashCodeImage = SecurityUtil.CalculateHash(serverFolder);

            //    model.CoverPhotoUrl = "\\" + folder;
            //}

            var shop = _addShopMapper.Map(model, hashCodeImage);

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

        public AddShopModel Get(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            return _addShopMapper.Map(shop);
        }

        public IList<ShopModel> GetAll()
        {
            var shops = _unitOfWork.ShopRepository.GetAll();

            var viewModel = new ShopViewModel();

            viewModel.Shops = new List<ShopModel>();

            foreach (var shop in shops)
            {
                var model = _shopMapper.Map(shop);

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public void Update(AddShopModel model)
        {
            var hashCodeImage = string.Empty;
            //string folder = string.Empty;
            //string serverFolder = string.Empty;

            //if (model.CoverPhoto == null)
            //{
            //    folder = model.CoverPhotoUrl;
            //    folder = folder.Substring(1);

            //    serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);                              
            //}
            //else
            //{
            //    folder = "images\\";
            //    folder += model.CoverPhoto.FileName;

            //    serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    var fileStream = new FileStream(serverFolder, FileMode.Create);

            //    model.CoverPhoto.CopyTo(fileStream);

            //    fileStream.Close();               
            //}

            //hashCodeImage = SecurityUtil.CalculateHash(serverFolder);

            //model.CoverPhotoUrl = "\\" + folder;

            var shop = _addShopMapper.Map(model, hashCodeImage);

            _unitOfWork.ShopRepository.Update(shop);
        }

        public bool IsExists(string name, int selectedCategory, int selectedCountry)
        {
            var shopName = _unitOfWork.ShopRepository.GetByCategoryId(name, selectedCategory, selectedCountry);

            return shopName != null;
        }
    }
}
