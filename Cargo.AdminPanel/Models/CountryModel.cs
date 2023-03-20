using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class CountryModel
    {
        public int Id { get; set; }     
        
        [Required(ErrorMessage = "Please enter a name!")]
        [MinLength(3, ErrorMessage = "Minimum Length must be 3!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only letters!")]
        public string Name { get; set; }
        public string CreationDateTime { get; set; }
    }
}
