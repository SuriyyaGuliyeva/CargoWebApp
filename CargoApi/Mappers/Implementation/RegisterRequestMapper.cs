using Cargo.Core.Domain.Entities;
using CargoApi.Mappers.Abstract;
using CargoApi.Models.AccountModels;

namespace CargoApi.Mappers.Implementation
{
    public class RegisterRequestMapper : IRegisterRequestMapper
    {
        public RegisterRequestModel Map(User user)
        {
            if (user == null)
                return null;

            var model = new RegisterRequestModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PinCode = user.PinCode,
                Address = user.Address
            };

            return model;
        }

        public User Map(RegisterRequestModel model)
        {
            if (model == null)
                return null;
            
            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                PasswordHash = model.Password,
                PhoneNumber = model.PhoneNumber,
                PinCode = model.PinCode,
                Address = model.Address
            };

            return user;
        }
    }
}
