using Microsoft.AspNetCore.Http;
using System;

namespace Cargo.Core.Domain.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }        
        public string ImageHashCode { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
