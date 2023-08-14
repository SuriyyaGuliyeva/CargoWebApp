using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITotalCountService _totalCountService;
        //private readonly ICountryService _countryService;
        //private readonly ICategoryService _categoryService;
        //private readonly IShopService _shopService;
        //private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserMapper userMapper, ITotalCountService totalCountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userMapper = userMapper;         
            _totalCountService = totalCountService;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userMapper.MapToRegisterModel(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded == false)
            {
                string[] errors = result.Errors.Select(x => x.Description).ToArray();
                string errorText = string.Join("\n", errors);

                ModelState.AddModelError(string.Empty, errorText);

                return View(model);
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            // for Auto Login
            await _signInManager.SignInAsync(user, true);

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManager.FindByNameAsync(model.Email).Result;

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is incorrect!");
                return View(model);
            }

            bool hasCorrectPassword = _userManager.CheckPasswordAsync(user, model.Password).Result;

            if (hasCorrectPassword == false)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is incorrect!");
                return View(model);
            }

            await _signInManager.SignInAsync(user, model.RememberMe);

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync()
                .GetAwaiter().GetResult();

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
