using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IUserService
    {
        Task AddToRoleAsync(SignInModel model, string roleName);

        Task<IdentityResult> CreateAsync(SignInModel model);

        public Task<IdentityResult> DeleteAsync(SignInModel model);

        Task<SignInModel> FindByIdAsync(string userId);

        Task<SignInModel> FindByNameAsync(string normalizedUserName);

        Task<string> GetNormalizedUserNameAsync(SignInModel model);

        Task<string> GetPasswordHashAsync(SignInModel model);

        Task<IList<string>> GetRolesAsync(SignInModel model);

        Task<string> GetUserIdAsync(SignInModel model);

        Task<string> GetUserNameAsync(SignInModel model);

        Task<IList<SignInModel>> GetUsersInRoleAsync(string roleName);

        Task<bool> HasPasswordAsync(SignInModel model);

        Task<bool> IsInRoleAsync(SignInModel model, string roleName);

        Task RemoveFromRoleAsync(SignInModel model, string roleName);

        Task SetNormalizedUserNameAsync(SignInModel model, string normalizedName);

        Task SetPasswordHashAsync(SignInModel model, string passwordHash);

        Task SetUserNameAsync(SignInModel model, string userName);

        Task<IdentityResult> UpdateAsync(SignInModel model);

        Task<bool> CheckPasswordAsync(SignInModel model, string password);

        //Task SignOutAsync();

        //Task SignInAsync(SignInModel model, bool isPersistent);

        void Dispose();
    }
}
