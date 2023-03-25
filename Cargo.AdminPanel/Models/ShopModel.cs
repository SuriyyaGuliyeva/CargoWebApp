using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        //[RegularExpression(@"[a-zA-Z0-9\\\-~!@#$%^*()_+{}:|""?`;',./[\]]+", ErrorMessage = "Please enter in the correct format!")]
        public string Name { get; set; }
        public string Link { get; set; }
        public string CreationDateTime { get; set; }

        //[Required]
        public CountryModel SelectedCountry { get; set; }

        //[Required]
        public CategoryModel SelectedCategory { get; set; }

        // Upload and Display Cover Photo
        //[Display(Name = "Choose the cover photo of the shop")]
        //[Required(ErrorMessage = "Please upload cover photo!")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
