using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
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
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ShopModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<ShopModel> GetAll()
        {
            var shops = _unitOfWork.ShopRepository.GetAll();

            var viewModel = new ShopViewModel();

            viewModel.Shops = new List<ShopModel>();

            foreach (var shop in shops)
            {
                var model = new ShopModel
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Link = shop.Link,
                    CountryName = shop.Country.Name,
                    CategoryName = shop.Category.Name,
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
            throw new System.NotImplementedException();
        }
    }
}
