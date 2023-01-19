using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CustomerCode { get; set; }
        public short TransportBalance { get; set; }
        public short OrderBalance { get; set; }
        public short MonthlyLimit { get; set; }
        public short TotalDebt { get; set; }          
        public string Photo { get; set; }           
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual User UserId { get; set; }
    }
}
