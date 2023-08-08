namespace Cargo.AdminPanel.Models
{
    public class ShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string CreationDateTime { get; set; }
        public CountryModel SelectedCountry { get; set; }
        public CategoryModel SelectedCategory { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
