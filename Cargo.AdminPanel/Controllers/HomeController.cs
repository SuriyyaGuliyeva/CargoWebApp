using Cargo.AdminPanel.Services.Abstract;
using Cargo.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Cargo.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountryService _countryService;
        private readonly ICategoryService _categoryService;
        private readonly IShopService _shopService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, ICountryService countryService, ICategoryService categoryService, IShopService shopService, IUserService userService)
        {
            _logger = logger;
            _countryService = countryService;
            _categoryService = categoryService;
            _shopService = shopService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            int totalCountryCount = _countryService.GetTotalCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _categoryService.GetTotalCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _shopService.GetTotalShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _userService.GetTotalUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
