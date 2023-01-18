﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Domain.Entities
{
    public class OrderStatusLog
    {
        [Key]
        public int Id { get; set; }
        public string NextStatus { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
