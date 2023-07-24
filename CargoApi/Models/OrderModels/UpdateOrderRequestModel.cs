namespace CargoApi.Models.OrderModels
{
    public class UpdateOrderRequestModel
    {
        public int Id { get; set; }
        public OrderRequestModel Order { get; set; }        

        //public int Count { get; set; }
        //public string Link { get; set; }
        //public decimal Price { get; set; }
        //public decimal CargoPrice { get; set; }
        //public int Size { get; set; }
        //public string Color { get; set; }
        //public string Note { get; set; }
        //public short TotalCount { get; set; }
        //public decimal TotalAmount { get; set; }
        //public int UserId { get; set; }
    }
}
