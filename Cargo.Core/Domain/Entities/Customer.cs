using System;

namespace Cargo.Core.Domain.Entities
{
    public class Customer
    {
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
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
