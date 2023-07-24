using Cargo.Core.DataAccessLayer.Abstract;
using CargoApi.Exceptions;
using CargoApi.Mappers.Abstract;
using CargoApi.Models.OrderModels;
using CargoApi.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoApi.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderMapper _mapper;
        private readonly IUpdateOrderMapper _updateOrderMapper;

        public OrderService(IUnitOfWork unitOfWork, IOrderMapper mapper, IUpdateOrderMapper updateOrderMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _updateOrderMapper = updateOrderMapper;
        }

        public void Add(OrderRequestModel model)
        {
            var order = _mapper.Map(model);

            _unitOfWork.OrderRepository.Add(order);
        }

        public void Delete(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id);

            if (order == null)
            {
                throw new AppException($"Order not found with ID: {id}");
            }

            _unitOfWork.OrderRepository.Delete(id);
        }        

        public OrderRequestModel Get(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id);

            if (order == null)
            {
                throw new AppException($"Order not found with ID: {id}");
            }

            var model = _mapper.Map(order);

            return model;
        }

        public IList<OrderRequestModel> GetAll()
        {
            var listOfOrderModel = new List<OrderRequestModel>();

            var orders = _unitOfWork.OrderRepository.GetAll();

            foreach (var order in orders)
            {
                var model = _mapper.Map(order);

                listOfOrderModel.Add(model);
            }

            return listOfOrderModel;
        }

        public void Update(UpdateOrderRequestModel model)
        {
            var orderModel = model.Order;

            var order = _mapper.Map(orderModel);
            order.Id = model.Id;

            _unitOfWork.OrderRepository.Update(order);
        }
    }
}
