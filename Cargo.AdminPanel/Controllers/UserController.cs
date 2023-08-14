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
        private readonly ITotalCountService _totalCountService;

        public UserController(UserManager<User> userManager, IUserService userService, ITotalCountService totalCountService)
        {
            _userManager = userManager;          
            _userService = userService;
            _totalCountService = totalCountService;
        }

        [TempData]
        public string Message { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var list = _userService.GetAll();

            ViewBag.Message = Message;

            return View(list);
        }  
        
        [HttpGet]
        public IActionResult Details(int userId)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var userModel = _userService.Get(userId);

            return View(userModel);
        }
    }
}
