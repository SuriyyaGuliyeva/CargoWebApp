using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Core.Domain.Entities
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
