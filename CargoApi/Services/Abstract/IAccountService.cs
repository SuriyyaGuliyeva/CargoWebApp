using CargoApi.Models.AccountModels;
using System.Threading.Tasks;

namespace CargoApi.Services.Abstract
{
    public interface IAccountService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel requestModel);           
    }
}
