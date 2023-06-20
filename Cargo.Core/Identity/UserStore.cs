using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.Identity
{
    public class UserStore : IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            _unitOfWork.UserRepository.AddToRoleAsync(user, roleName, cancellationToken);

            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _unitOfWork.UserRepository.CreateAsync(user, cancellationToken);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.UserRepository.Dispose();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.FindByIdAsync(userId, cancellationToken);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetPasswordHashAsync(user, cancellationToken);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetRolesAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetUserNameAsync(user, cancellationToken);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetUsersInRoleAsync(roleName, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.HasPasswordAsync(user, cancellationToken);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.IsInRoleAsync(user, roleName, cancellationToken);
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.RemoveFromRoleAsync(user, roleName, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        }
    }
}
