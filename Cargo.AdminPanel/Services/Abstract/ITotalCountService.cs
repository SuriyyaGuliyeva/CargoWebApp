namespace Cargo.AdminPanel.Services.Abstract
{
    public interface ITotalCountService
    {
        int GetCountryCount();
        int GetCategoryCount();
        int GetShopCount();
        int GetUserCount();
    }
}
