using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo.Core.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public string Link { get; set; }
        public short Price { get; set; }
        public short CargoPrice { get; set; }
        public double Size { get; set; }
        public string Color { get; set; }
        public string Note { get; set; }
        public short TotalCount { get; set; }
        public short TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("OrderStatusLog")]
        public int OrderStatusLogId { get; set; }
        public OrderStatusLog OrderStatusLog { get; set; }

    }
}
