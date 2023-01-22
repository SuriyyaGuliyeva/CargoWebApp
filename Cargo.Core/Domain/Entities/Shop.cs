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
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
