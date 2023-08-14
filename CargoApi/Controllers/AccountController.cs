using CargoApi.Models.AccountModels;
using CargoApi.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CargoApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
        {
            var response = await _accountService.Login(requestModel);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel)
        {
            await _accountService.Register(requestModel);

            // for Auto Login
            var loginResponse = await _accountService.Login(
                new LoginRequestModel
                {
                    Email = requestModel.Email,
                    Password = requestModel.Password
                }
            );

            return StatusCode(StatusCodes.Status201Created, loginResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ProfileDetails()
        {
            var responseModel = await _accountService.ProfileDetails();

            return StatusCode(StatusCodes.Status200OK, responseModel);
        }
    }
}
