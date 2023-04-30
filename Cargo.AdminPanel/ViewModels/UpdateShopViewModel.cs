using Cargo.AdminPanel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cargo.AdminPanel.ViewModels
{
    public class UpdateShopViewModel
    {
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public UpdateShopModel Shop { get; set; }
    }
}
