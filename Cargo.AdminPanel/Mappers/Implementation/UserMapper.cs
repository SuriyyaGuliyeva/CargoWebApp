using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class UserMapper : IUserMapper
    {
        public SignInModel Map(User user)
        {
            var model = new SignInModel
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash
            };

            return model;
        }

        public User Map(SignInModel model)
        {
            var user = new User
            {
                Id = model.Id,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                PasswordHash = model.Password
            };

            return user;
        }

        public User MapToRegisterModel(RegisterModel model)
        {
            var user = new User
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                PasswordHash = model.Password,
                PhoneNumber = model.PhoneNumber
            };

            return user;
        }

        public RegisterModel MapToUser(User user)
        {
            var model = new RegisterModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,                
                Email = user.Email,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                PhoneNumber = user.PhoneNumber               
            };

            return model;
        }

        public UserModel MapUser(User user)
        {
            if (user == null)
                return null;

            var model = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return model;
        }

        public User MapUserModel(UserModel model)
        {
            if (model == null)
                return null;

            var user = new User
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                PhoneNumber = model.PhoneNumber
            };

            return user;
        }
    }
}
