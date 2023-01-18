using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Domain.Entities
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Country> Countries { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
