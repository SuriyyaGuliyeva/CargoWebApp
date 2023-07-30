namespace CargoApi.Models.OrderModels
{
    public class UpdateOrderRequestModel
    {
        public int Id { get; set; }
        public OrderRequestModel Order { get; set; }
    }
}
