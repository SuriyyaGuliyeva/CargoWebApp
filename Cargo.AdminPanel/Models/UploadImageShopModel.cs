using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cargo.AdminPanel.Models
{
    public class UploadImageShopModel
    {
        public int Id { get; set; }       

        [Display(Name = "Choose the cover photo of the shop")]
        [Required(ErrorMessage = "Please upload cover photo!")]
        public IFormFile CoverPhoto { get; set; }
    }
}
