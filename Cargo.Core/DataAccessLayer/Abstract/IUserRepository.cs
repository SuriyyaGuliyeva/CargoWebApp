using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepository
    {
        Task AddToRoleAsync(User user, string roleName);

        Task<IdentityResult> CreateAsync(User user);

        public Task<IdentityResult> DeleteAsync(User user);

        Task<User> FindByIdAsync(string userId);

        Task<User> FindByNameAsync(string normalizedUserName);

        Task<string> GetNormalizedUserNameAsync(User user);

        Task<string> GetPasswordHashAsync(User user);

        Task<IList<string>> GetRolesAsync(User user);

        Task<string> GetUserIdAsync(User user);

        Task<string> GetUserNameAsync(User user);

        Task<IList<User>> GetUsersInRoleAsync(string roleName);

        Task<bool> HasPasswordAsync(User user);

        Task<bool> IsInRoleAsync(User user, string roleName);

        Task RemoveFromRoleAsync(User user, string roleName);

        Task SetNormalizedUserNameAsync(User user, string normalizedName);

        Task SetPasswordHashAsync(User user, string passwordHash);

        Task SetUserNameAsync(User user, string userName);

        Task<IdentityResult> UpdateAsync(User user);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task SignOutAsync();

        Task SignInAsync(User user, bool isPersistent);

        void Dispose();
    }
}
