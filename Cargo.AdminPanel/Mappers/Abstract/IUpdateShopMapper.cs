using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface IUpdateShopMapper
    {
        UpdateShopModel Map(Shop shop);
        Shop Map(UpdateShopModel model);
    }
}
