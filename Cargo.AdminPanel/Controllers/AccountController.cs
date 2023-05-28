using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
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

            var user = _userManager.FindByNameAsync(model.Username).Result;
            var user2 = _userService.FindByNameAsync(model.Username).Result;

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect!");
                return View(model);
            }

            bool hasCorrectPassword = _userManager.CheckPasswordAsync(user, model.PasswordHash).Result;
            //bool hasCorrectPassword2 = _userService.CheckPasswordAsync(user2, model.PasswordHash).Result;

            if (hasCorrectPassword == false)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect!");
                return View(model);
            }

            await _signInManager.SignInAsync(user, model.RememberMe);

            //await _userService.SignInAsync(user2, model.RememberMe);
            

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync()
                .GetAwaiter().GetResult();

            //_userService.SignOutAsync();

            return Redirect("/");
        }
    }
}
