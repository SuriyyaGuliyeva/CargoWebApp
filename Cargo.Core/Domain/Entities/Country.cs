﻿using System;

namespace Cargo.Core.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
