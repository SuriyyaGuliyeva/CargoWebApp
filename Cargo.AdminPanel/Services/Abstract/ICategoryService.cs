using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface ICategoryService
    {
        void Add(AddCategoryViewModel model);
        void Update(AddCategoryViewModel model);
        void Delete(int id);
        IList<CategoryModel> GetAll();
        AddCategoryViewModel Get(int id);
        bool IsExists(CategoryModel model);
    }
}
