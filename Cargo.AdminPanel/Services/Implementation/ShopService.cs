using Cargo.AdminPanel.Helpers;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
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
            var shop = _addShopMapper.Map(model);

            Directory.CreateDirectory(StorageConstants.ShopsPhotoDirectory);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Step 1. Read photo to memory
                model.CoverPhoto.CopyTo(memoryStream);

                // Step 2. Calculate hash for photo
                var hash = SecurityUtil.CalculateHash(memoryStream.ToArray());

                // Step 3. Set photo name
                shop.Photo = $"{hash}.jpg";

                // Step 4. Create file path for photo
                var filePath = Path.Combine(StorageConstants.ShopsPhotoDirectory, shop.Photo);

                // Step 5. Check this file already exists or not
                if (File.Exists(filePath) == false)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        memoryStream.WriteTo(fileStream);
                    }
                }
            }

            _unitOfWork.ShopRepository.Add(shop);
        }

        public void Delete(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            if (shop == null)
                throw new Exception("Shop not found");

            _unitOfWork.ShopRepository.Delete(id);
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
            var shop = _addShopMapper.Map(model);

            _unitOfWork.ShopRepository.Update(shop);
        }

        public bool IsExists(string name, int selectedCategory, int selectedCountry)
        {
            var shopName = _unitOfWork.ShopRepository.GetByCategoryId(name, selectedCategory, selectedCountry);

            return shopName != null;
        }
    }
}
