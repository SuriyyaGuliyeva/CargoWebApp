using Cargo.Core.Domain.Entities;
using CargoApi.Mappers.Abstract;
using CargoApi.Models.AccountModels;

namespace CargoApi.Mappers.Implementation
{
    public class ProfileDetailsResponseMapper : IProfileDetailsResponseMapper
    {           
        public User Map(ProfileDetailsResponseModel model)
        {
            if (model == null)
                return null;

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                PhoneNumber = model.PhoneNumber,
                PinCode = model.PinCode,
                Address = model.Address
            };

            return user;
        }

        public ProfileDetailsResponseModel Map(User user)
        {
            if (user == null)
                return null;

            var model = new ProfileDetailsResponseModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,               
                PhoneNumber = user.PhoneNumber,
                PinCode = user.PinCode,
                Address = user.Address
            };

            return model;
        }
    }
}
