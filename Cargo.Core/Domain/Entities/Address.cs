using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.Core.Domain.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public virtual Country CountryId { get; set; }
        public string FullName { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string AddressHeading { get; set; }
        public string Line1 { get; set; }
        public string District2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string TcIdentity { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
