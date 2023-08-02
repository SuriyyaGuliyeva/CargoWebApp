using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]        
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Minimum length should be 5")]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Minimum length should be 5")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number is not valid")] 
        public string PhoneNumber { get; set; }
    }
}
