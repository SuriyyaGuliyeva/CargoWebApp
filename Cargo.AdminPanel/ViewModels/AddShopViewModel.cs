using Cargo.AdminPanel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargo.AdminPanel.ViewModels
{
    public class AddShopViewModel
    {
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public AddShopModel Shop { get; set; }
    }
}
