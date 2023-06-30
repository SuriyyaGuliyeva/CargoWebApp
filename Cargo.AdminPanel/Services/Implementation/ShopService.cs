using Cargo.AdminPanel.Helpers;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.Constants;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopMapper _shopMapper;
        private readonly IAddShopMapper _addShopMapper;
        private readonly IUpdateShopMapper _updateShopMapper;

        public ShopService(IUnitOfWork unitOfWork, IShopMapper shopMapper, IAddShopMapper addShopMapper, IUpdateShopMapper updateShopMapper)
        {
            _unitOfWork = unitOfWork;
            _shopMapper = shopMapper;
            _addShopMapper = addShopMapper;
            _updateShopMapper = updateShopMapper;
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
            {
                throw new Exception("not found");
            }

            _unitOfWork.ShopRepository.Delete(id);
        }

        public AddShopModel GetAddModel(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            var model = _addShopMapper.Map(shop);

            return model;
        }

        public UpdateShopModel GetUpdateModel(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            var model = _updateShopMapper.Map(shop);

            return model;
        }

        public UploadImageShopModel GetUploadImageModel(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            var model = new UploadImageShopModel
            {
                Id = shop.Id
            };

            return model;
        }

        public ShopModel Get(int id)
        {
            var shop = _unitOfWork.ShopRepository.Get(id);

            var model = _shopMapper.Map(shop);

            return model;
        }

        public IList<ShopModel> GetAll()
        {
            var shops = _unitOfWork.ShopRepository.GetAll();

            var viewModel = new ShopViewModel();

            viewModel.Shops = new List<ShopModel>();

            foreach (var shop in shops)
            {
                string filePath = Path.Combine(StorageConstants.ShopsPhotoDirectory, shop.Photo);

                shop.Photo = filePath;

                var model = _shopMapper.Map(shop);             

                viewModel.Shops.Add(model);
            }

            return viewModel.Shops;
        }

        public void Update(UpdateShopModel model)
        {
            var shop = _updateShopMapper.Map(model);

            _unitOfWork.ShopRepository.Update(shop);
        }

        public void UploadNewImage(UploadImageShopModel model)
        {
            Directory.CreateDirectory(StorageConstants.ShopsPhotoDirectory);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Step 1. Read photo to memory
                model.CoverPhoto.CopyTo(memoryStream);

                // Step 2. Calculate hash for photo
                var hash = SecurityUtil.CalculateHash(memoryStream.ToArray());

                // Step 3. Set photo name
                var photo = $"{hash}.jpg";

                // Step 4. Create file path for photo
                var filePath = Path.Combine(StorageConstants.ShopsPhotoDirectory, photo);

                // Step 5. Check this file already exists or not
                if (File.Exists(filePath) == false)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        memoryStream.WriteTo(fileStream);
                    }
                }

                // Step 6. Update in database
                var shop = _unitOfWork.ShopRepository.Get(model.Id);

                shop.Photo = photo;

                _unitOfWork.ShopRepository.Update(shop);
            }
        }

        public bool IsExists(string name, int selectedCategory, int selectedCountry)
        {
            var shopName = _unitOfWork.ShopRepository.GetByCategoryId(name, selectedCategory, selectedCountry);

            return shopName != null;
        }
    }
}
