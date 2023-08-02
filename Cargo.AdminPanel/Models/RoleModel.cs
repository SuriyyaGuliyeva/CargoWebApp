using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
