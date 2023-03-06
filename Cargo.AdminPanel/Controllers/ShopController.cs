using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public IActionResult Index()
        {
            var viewModel = new ShopViewModel();

            viewModel.Shops = _shopService.GetAll();

            return View(viewModel);
        }
    }
}
