using Cargo.Core.Domain.Entities;
using CargoApi.Mappers.Abstract;
using CargoApi.Models.OrderModels;

namespace CargoApi.Mappers.Implementation
{
    public class UpdateOrderMapper : IUpdateOrderMapper
    {
        public UpdateOrderRequestModel Map(Order order)
        {
            if (order == null)
                return null;

            var model = new UpdateOrderRequestModel
            {
                Id = order.Id,
                //Count = order.Count,
                //Link = order.Link,
                //Price = order.Price,
                //CargoPrice = order.CargoPrice,
                //Size = order.Size,
                //Color = order.Color,
                //Note = order.Note,
                //TotalCount = order.TotalCount,
                //TotalAmount = order.TotalAmount,
                //UserId = order.UserId
            };

            return model;
        }

        public Order Map(UpdateOrderRequestModel model)
        {
            if (model == null)
                return null;

            var order = new Order
            {
                Id = model.Id,
                //Count = model.Count,
                //Link = model.Link,
                //Price = model.Price,
                //CargoPrice = model.CargoPrice,
                //Size = model.Size,
                //Color = model.Color,
                //Note = model.Note,
                //TotalCount = model.TotalCount,
                //TotalAmount = model.TotalAmount,
                //UserId = model.UserId
            };

            return order;
        }
    }
}
