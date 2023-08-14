using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleService _roleService;
        private readonly ITotalCountService _totalCountService;

        public RoleController(RoleManager<Role> roleManager, IRoleService roleService, ITotalCountService totalCountService)
        {
            _roleManager = roleManager;
            _roleService = roleService;
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

            var list = _roleService.GetAll();

            ViewBag.Message = Message;

            return View(list);
        }

        [HttpGet]
        public IActionResult Update(int roleId)
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            var model = _roleService.Get(roleId);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(RoleModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            if (_roleService.IsExists(model))
            {
                ViewBag.IsExistName = "This role name already exists!";
                return View(model);
            }

            _roleService.Update(model);

            Message = "Successfully Updated!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            int totalCountryCount = _totalCountService.GetCountryCount();
            ViewBag.TotalCountryCount = totalCountryCount;

            int totalCategoryCount = _totalCountService.GetCategoryCount();
            ViewBag.TotalCategoryCount = totalCategoryCount;

            int totalShopCount = _totalCountService.GetShopCount();
            ViewBag.TotalShopCount = totalShopCount;

            int totalUserCount = _totalCountService.GetUserCount();
            ViewBag.TotalUserCount = totalUserCount;

            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            if (_roleService.IsExists(model))
            {
                ViewBag.IsExistName = "This role name already exists!";

                return View(model);
            }

            _roleService.Add(model);

            Message = "Successfully Added!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int roleId)
        {
            _roleService.Delete(roleId);

            Message = "Successfully Deleted!";

            return RedirectToAction(nameof(Index));
        }
    }
}
