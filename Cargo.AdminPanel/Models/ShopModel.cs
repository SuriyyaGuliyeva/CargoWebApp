using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public CountryModel SelectedCountry { get; set; }
        public CategoryModel SelectedCategory { get; set; }

        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
