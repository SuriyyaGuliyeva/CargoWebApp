using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class AddShopMapper : IAddShopMapper
    {
        public AddShopModel Map(Shop shop) 
        {
            if (shop == null)
                return null;

            AddShopModel model = new AddShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Link = shop.Link,
                SelectedCountry = shop.CountryId,
                SelectedCategory =shop.CategoryId,
            };

            return model;
        }

        public Shop Map(AddShopModel model)
        {
            if (model == null)
                return null;

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                CountryId = model.SelectedCountry,
                CategoryId = model.SelectedCategory
            };

            return shop;
        }
    }
}
