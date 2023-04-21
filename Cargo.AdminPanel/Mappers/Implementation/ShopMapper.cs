using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class ShopMapper : IShopMapper
    {
        private readonly ICountryMapper _countryMapper;
        private readonly ICategoryMapper _categoryMapper;

        public ShopMapper(ICountryMapper countryMapper, ICategoryMapper categoryMapper)
        {
            _countryMapper = countryMapper;
            _categoryMapper = categoryMapper;
        }

        public ShopModel Map(Shop shop) 
        {
            if (shop == null)
                return null;            

            ShopModel model = new ShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Link = shop.Link,
                SelectedCountry = _countryMapper.Map(shop.Country),
                SelectedCategory = _categoryMapper.Map(shop.Category),
                CoverPhotoUrl = shop.Photo,
            };

            return model;
        }

        public Shop Map(ShopModel model, string hashCodeImage)
        {
            if (model == null)
                return null;

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Photo = model.CoverPhotoUrl, 
                CountryId = model.SelectedCountry.Id,
                CategoryId = model.SelectedCategory.Id
            };

            return shop;
        }
    }
}
