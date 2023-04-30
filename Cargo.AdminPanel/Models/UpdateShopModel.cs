using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class UpdateShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }
        public string Link { get; set; }

        [Required(ErrorMessage = "Please enter a country name!")]
        public int SelectedCountry { get; set; }

        [Required(ErrorMessage = "Please enter a category name!")]
        public int SelectedCategory { get; set; }      
    }
}
