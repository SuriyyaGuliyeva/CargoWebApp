using Cargo.Core.Domain.Entities;
using CargoApi.Models.OrderModels;

namespace CargoApi.Mappers.Abstract
{
    public interface IOrderMapper
    {
        OrderRequestModel Map(Order order);
        Order Map(OrderRequestModel model);      
    }
}
