using System.ComponentModel.DataAnnotations;

namespace Library.Core.Domain.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public virtual User UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
