using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Core.Domain.Entities
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
        public DateTime CreationDateTime { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
