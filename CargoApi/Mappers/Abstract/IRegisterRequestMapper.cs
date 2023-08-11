using Cargo.Core.Domain.Entities;
using CargoApi.Models.AccountModels;

namespace CargoApi.Mappers.Abstract
{
    public interface IRegisterRequestMapper
    {
        RegisterRequestModel Map(User user);
        User Map(RegisterRequestModel model);
    }
}
