using Cargo.AdminPanel.Models;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IShopService
    {
        void Add(ShopModel model);
        void Update(ShopModel model);
        void Delete(int id);
        IList<ShopModel> GetAll();
        ShopModel Get(int id);
        ShopModel GetByCategoryId(string name, int categoryId);
    }
}
