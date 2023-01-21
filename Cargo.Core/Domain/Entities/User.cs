using System.Collections.Generic;

namespace Cargo.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
