using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        [MinLength(4, ErrorMessage = "Minimum Length must be 4!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only letters!")]
        public string Name { get; set; }
        public string CreationDateTime { get; set; }
    }
}
