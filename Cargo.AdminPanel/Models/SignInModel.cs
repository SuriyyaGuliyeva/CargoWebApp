using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class SignInModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = true;
    }
}
