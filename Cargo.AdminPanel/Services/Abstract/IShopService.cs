using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IShopService
    {
        void Add(AddShopViewModel model);
        void Update(ShopModel model);
        void Delete(int id);
        IList<ShopModel> GetAll();
        ShopModel Get(int id);
        bool IsExists(ShopModel model);
        //ShopModel GetByCategoryId(string name, int categoryId);
    }
}
