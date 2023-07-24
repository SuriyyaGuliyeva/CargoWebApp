using CargoApi.Models.OrderModels;
using System.Collections.Generic;
using System.Threading.Tasks;

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
