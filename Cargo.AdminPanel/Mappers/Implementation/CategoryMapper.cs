using Cargo.AdminPanel.Constants;
using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryModel Map(Category category)
        {
            if (category == null)
                return null;

            var model = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                CreationDateTime = category.CreationDateTime.ToString(SystemConstants.DateTimeParseFormat)
            };

            return model;
        }

        public Category Map(CategoryModel model)
        {
            if (model == null)
                return null;

            var category = new Category
            {
                Id = model.Id,
                Name = model.Name
            };

            return category;
        }
    }
}
