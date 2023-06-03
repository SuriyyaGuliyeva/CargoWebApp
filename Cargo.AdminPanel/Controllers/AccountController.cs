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

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect!");
                return View(model);
            }

            bool hasCorrectPassword = _userManager.CheckPasswordAsync(user, model.PasswordHash).Result;

            if (hasCorrectPassword == false)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect!");
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
    }
}
