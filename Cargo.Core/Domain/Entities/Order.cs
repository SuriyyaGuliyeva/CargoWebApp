using System;

namespace Cargo.Core.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Count { get; set; } = 1;
        public string Link { get; set; }
        public decimal Price { get; set; }
        public decimal CargoPrice { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public string Note { get; set; }
        public short TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
