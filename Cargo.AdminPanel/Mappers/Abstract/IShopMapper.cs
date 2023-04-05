using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface IShopMapper
    {
        ShopModel Map(Shop shop);
        Shop Map(ShopModel model, string hashCodeImage);
    }
}
