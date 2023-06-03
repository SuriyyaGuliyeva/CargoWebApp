using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class SignInModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        [MaxLength(30)]
        public string PasswordHash { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}
