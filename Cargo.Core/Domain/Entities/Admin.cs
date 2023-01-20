﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Core.Domain.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public virtual User UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}