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
            var id = _unitOfWork.UserRepository.Add(user);

            cancellationToken.ThrowIfCancellationRequested();

            if (id > 0)
                return Task.FromResult(IdentityResult.Success);

            var identityError = new IdentityError
            {
                Description = "Can not add user"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var success = _unitOfWork.UserRepository.Delete(user.Id);

            cancellationToken.ThrowIfCancellationRequested();

            if (success)
                return Task.FromResult(IdentityResult.Success);

            var identityError = new IdentityError
            {
                Description = "Can not delete user"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public void Dispose()
        {
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
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetRolesAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return _unitOfWork.UserRepository.GetUsersInRoleAsync(roleName, cancellationToken);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
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
            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Name = userName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var success = _unitOfWork.UserRepository.Update(user);

            if (success)
                return Task.FromResult(IdentityResult.Success);

            var identityError = new IdentityError
            {
                Description = "Can not update user"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }
    }
}
