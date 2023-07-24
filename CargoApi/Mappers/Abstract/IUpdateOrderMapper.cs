using Cargo.Core.Domain.Entities;
using CargoApi.Models.OrderModels;

namespace CargoApi.Mappers.Abstract
{
    public interface IUpdateOrderMapper
    {
        UpdateOrderRequestModel Map(Order order);
        Order Map(UpdateOrderRequestModel model);
    }
}
