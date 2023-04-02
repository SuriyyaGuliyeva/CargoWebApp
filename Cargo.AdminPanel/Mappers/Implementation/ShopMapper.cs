using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class ShopMapper : IShopMapper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryMapper _countryMapper;
        private readonly ICategoryMapper _categoryMapper;

        public ShopMapper(IUnitOfWork unitOfWork, ICountryMapper countryMapper, ICategoryMapper categoryMapper)
        {
            _unitOfWork = unitOfWork;
            _countryMapper = countryMapper;
            _categoryMapper = categoryMapper;
        }

        public ShopModel Map(Shop shop, Country country, Category category) 
        {
            if (shop == null)
            {
                return null;
            }

            ShopModel model = new ShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Link = shop.Link,
                SelectedCountry = _countryMapper.Map(country),
                SelectedCategory = _categoryMapper.Map(category),
                CoverPhotoUrl = shop.Photo,
                CreationDateTime = shop.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            return model;
        }

        public Shop Map(ShopModel model, string hashCodeImage)
        {
            if (model == null)
            {
                return null;
            }           

            var shop = new Shop
            {
                Id = model.Id,
                Name = model.Name,
                Link = model.Link,
                Photo = model.CoverPhotoUrl, 
                ImageHashCode = hashCodeImage,
                CountryId = model.SelectedCountry.Id,
                CategoryId = model.SelectedCategory.Id
            };

            return shop;
        }
    }
}
