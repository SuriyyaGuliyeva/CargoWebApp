using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Domain.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkHours { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
