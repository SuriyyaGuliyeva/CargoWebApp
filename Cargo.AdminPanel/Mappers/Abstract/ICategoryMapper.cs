using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface ICategoryMapper
    {
        CategoryModel Map(Category category);
        Category Map(CategoryModel model);
    }
}
