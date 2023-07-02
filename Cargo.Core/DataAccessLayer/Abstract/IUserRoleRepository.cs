using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRoleRepository
    {
        void AddToRole(User user, string roleName);
        IList<string> GetRoles(User user);
        IList<User> GetUsersInRole(string roleName);
        bool IsInRole(User user, string roleName);
        void RemoveFromRole(User user, string roleName);
    }
}
