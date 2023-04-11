using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cargo.AdminPanel.Models
{
    public class AddShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }
        public string Link { get; set; }

        [Required(ErrorMessage = "Please enter a country name!")]
        public int SelectedCountry { get; set; }

        [Required(ErrorMessage = "Please enter a category name!")]
        public int SelectedCategory { get; set; }

        // Upload and Display Cover Photo
        [Display(Name = "Choose the cover photo of the shop")]
        [Required(ErrorMessage = "Please upload cover photo!")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }

    }

}
