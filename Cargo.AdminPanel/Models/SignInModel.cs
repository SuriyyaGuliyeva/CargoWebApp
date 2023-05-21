using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public bool RememberMe { get; set; }
    }
}
