using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository : IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>
    {      
    }
}
