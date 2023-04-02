using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }
        public string Link { get; set; }
        public string CreationDateTime { get; set; }

        //[Required(ErrorMessage = "Please enter a country name!")]
        public CountryModel SelectedCountry { get; set; }

        //[Required(ErrorMessage = "Please enter a category name!")]
        public CategoryModel SelectedCategory { get; set; }

        // Upload and Display Cover Photo
        [Display(Name = "Choose the cover photo of the shop")]
        [Required(ErrorMessage = "Please upload cover photo!")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
