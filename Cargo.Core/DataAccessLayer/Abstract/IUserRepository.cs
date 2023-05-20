using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository
    {
        User FindByUsername(string username);

        User FindByUsernameAndPassword(string username, string password);

        int Add(User user);

        bool Delete(User user);

        User FindById(string userId);

    }
}
