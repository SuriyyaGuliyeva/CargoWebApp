using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string CountryName { get; set; }
        public string CategoryName { get; set; }
        public string Photo { get; set; }
        public string CreationDateTime { get; set; }

        // Dropdown List for Country
        public string SelectedCountry { get; set; }
        public List<SelectListItem> CountriesSelectList { get; set; }

        // Dropdown List for Category
        public string SelectedCategory { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
    }
}
