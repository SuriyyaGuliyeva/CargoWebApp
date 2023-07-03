using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        int AddToRole(UserRole userRole);
        IList<string> GetRoles(int userId);
        IList<User> GetUsersInRole(string roleName);
        bool IsInRole(User user, string roleName);
        void RemoveFromRole(int userId, string roleName);
    }
}
