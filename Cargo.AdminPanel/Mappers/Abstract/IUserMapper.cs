using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface IUserMapper
    {
        SignInModel Map(User user);

        User Map(SignInModel model);

        RegisterModel MapToUser(User user);

        User MapToRegisterModel(RegisterModel model);
    }
}
