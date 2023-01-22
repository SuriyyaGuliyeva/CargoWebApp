using System;
using System.Collections.Generic;

namespace Cargo.Core.Domain.Entities
{
    public class OrderStatusLog
    {
        public int Id { get; set; }
        public string NextStatus { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        //public ICollection<Order> Orders { get; set; }
    }
}
