using System;
using System.Collections.Generic;

namespace Cargo.Core.Domain.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
