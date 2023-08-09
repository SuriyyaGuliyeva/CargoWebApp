using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ICountryService _countryService;
        private readonly ICategoryService _categoryService;
        private readonly IShopService _shopService;

        public UserController(UserManager<User> userManager, ICountryService countryService, ICategoryService categoryService, IShopService shopService, IUserService userService)
        {
            _userManager = userManager;
            _countryService = countryService;
            _categoryService = categoryService;
            _shopService = shopService;
            _userService = userService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
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

            var list = _userService.GetAll();

            ViewBag.Message = Message;

            return View(list);
        }  
        
        [HttpGet]
        public IActionResult Details(int userId)
        {
            int totalCountryCount = _countryService.GetTotalCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _categoryService.GetTotalCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _shopService.GetTotalShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _userService.GetTotalUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var userModel = _userService.Get(userId);

            return View(userModel);
        }
    }
}
