using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IShopService
    {
        void Add(AddShopModel model);
        void Update(AddShopModel model);
        void Delete(int id);
        IList<ShopModel> GetAll();
        AddShopModel Get(int id);
        bool IsExists(string name, int categoryId, int countryId);
    }
}
