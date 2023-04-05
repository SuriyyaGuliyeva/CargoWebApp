using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface IAddShopMapper
    {
        AddShopModel Map(Shop shop);
        Shop Map(AddShopModel model, string hashCodeImage);
    }
}
