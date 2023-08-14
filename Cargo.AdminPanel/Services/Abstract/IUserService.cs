using Cargo.AdminPanel.Models;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IUserService
    {
        IList<UserModel> GetAll();
        UserModel Get(int id);
    }
}
