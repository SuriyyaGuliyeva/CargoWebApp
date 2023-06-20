using CargoApi.Models.AccountModels;
using CargoApi.Services.Abstract;
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
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestModel requestModel)
        {
            var response = await _accountService.Authenticate(requestModel);

            return Ok(response);
        }
    }
}
