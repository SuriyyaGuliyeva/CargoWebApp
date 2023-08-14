using CargoApi.Models.AccountModels;
using System.Threading.Tasks;

namespace CargoApi.Services.Abstract
{
    public interface IAccountService
    {
        Task<LoginResponseModel> Login(LoginRequestModel requestModel);           
        Task Register(RegisterRequestModel requestModel);
        Task<ProfileDetailsResponseModel> ProfileDetails();
    }
}
