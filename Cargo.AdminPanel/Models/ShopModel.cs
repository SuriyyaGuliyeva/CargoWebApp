using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name!")]
        [RegularExpression(@"^[a-zA-Z0-9_&\s]+$", ErrorMessage = "Please enter letters, digits or & _ symbols!")]
        public string Name { get; set; }
        public string Link { get; set; }
        public string CountryName { get; set; }
        public string CategoryName { get; set; }       
        public string CreationDateTime { get; set; }

        // Dropdown List for Country
        [Required(ErrorMessage = "Please enter a country name!")]
        public string SelectedCountry { get; set; }
        public List<SelectListItem> CountriesSelectList { get; set; }

        // Dropdown List for Category
        [Required(ErrorMessage = "Please enter a category name!")]
        public string SelectedCategory { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }

        /////////////////////
        //public string Photo { get; set; }

        [Display(Name="Choose the cover photo of the shop")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
