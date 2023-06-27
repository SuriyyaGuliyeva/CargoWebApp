using CargoApi.Models.AccountModels;
using CargoApi.Services.Abstract;
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
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestModel requestModel)
        {
            var response = await _accountService.Authenticate(requestModel);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel)
        {
            await _accountService.Register(requestModel);

            // for Auto Login
            var loginResponse = await _accountService.Authenticate(
                new LoginRequestModel
                {
                    Name = requestModel.Name,
                    PasswordHash = requestModel.PasswordHash
                }
            );

            return StatusCode(StatusCodes.Status201Created, loginResponse);
        }
    }
}
