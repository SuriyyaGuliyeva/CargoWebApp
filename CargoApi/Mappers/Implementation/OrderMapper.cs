using Cargo.Core.Constants;
using Cargo.Core.Domain.Entities;
using CargoApi.Mappers.Abstract;
using CargoApi.Models.OrderModels;

namespace CargoApi.Mappers.Implementation
{
    public class OrderMapper : IOrderMapper
    {
        public OrderRequestModel Map(Order order)
        {
            if (order == null)
                return null;

            var model = new OrderRequestModel
            {
                Count = order.Count,
                Link = order.Link,
                Price = order.Price,
                CargoPrice = order.CargoPrice,
                Size = order.Size,
                Color = order.Color,
                Note = order.Note,
                TotalCount = order.TotalCount,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId
            };

            return model;
        }

        public Order Map(OrderRequestModel model)
        {
            if (model == null)
                return null;

            var order = new Order
            {
                Count = model.Count,
                Link = model.Link,
                Price = model.Price,
                CargoPrice = model.CargoPrice,
                Size = model.Size,
                Color = model.Color,
                Note = model.Note,
                TotalCount = model.TotalCount,
                TotalAmount = model.TotalAmount,
                UserId = model.UserId
            };

            return order;
        }
    }
}
