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
                Username = user.Name,
                PasswordHash = user.PasswordHash
            };

            return model;
        }

        public User Map(SignInModel model)
        {
            var user = new User
            {
                Id = model.Id,
                Name = model.Username,
                NormalizedUserName = model.Username.ToUpper(),
                PasswordHash = model.PasswordHash
            };

            return user;
        }
    }
}
