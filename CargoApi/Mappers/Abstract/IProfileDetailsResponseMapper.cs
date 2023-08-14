using Cargo.Core.Domain.Entities;
using CargoApi.Models.AccountModels;

namespace CargoApi.Mappers.Abstract
{
    public interface IProfileDetailsResponseMapper
    {
        ProfileDetailsResponseModel Map(User user);
        User Map(ProfileDetailsResponseModel model);
    }
}
