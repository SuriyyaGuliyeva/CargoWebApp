﻿using System;

namespace Cargo.Core.Domain.Entities
{
    public class Admin
    {       
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
