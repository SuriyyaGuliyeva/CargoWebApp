using CargoApi.Models.OrderModels;
using System.Collections.Generic;

namespace CargoApi.Services.Abstract
{
    public interface IOrderService
    {
        void Add(OrderRequestModel model);
        void Update(UpdateOrderRequestModel model);
        void Delete(int id);
        IList<OrderRequestModel> GetAll();
        OrderRequestModel Get(int id);
    }
}
