using CargoApi.Models.OrderModels;
using CargoApi.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Add(OrderRequestModel model)
        {
            _orderService.Add(model);

            return StatusCode(StatusCodes.Status201Created, model);
        }       

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           _orderService.Delete(id);

            return NoContent();            
        }

        [HttpPut]
        public IActionResult Update(UpdateOrderRequestModel model)
        {
            _orderService.Update(model);

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAll();

            return Ok(orders);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var order = _orderService.Get(id);

            return Ok(order);
        }
    }
}
