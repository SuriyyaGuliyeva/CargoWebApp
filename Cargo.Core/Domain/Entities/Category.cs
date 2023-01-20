using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Core.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
