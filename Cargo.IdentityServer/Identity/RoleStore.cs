using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.Identity
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var id = _unitOfWork.RoleRepository.Add(role);
            role.Id = id;

            if (id > 0)
                return Task.FromResult(IdentityResult.Success);

            return AddErrorMessage("Can not add role");
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var success = _unitOfWork.RoleRepository.Delete(role.Id);

            if (success)
                return Task.FromResult(IdentityResult.Success);

            return AddErrorMessage("Can not delete role");
        }

        public void Dispose()
        {
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = _unitOfWork.RoleRepository.Get(Convert.ToInt32(roleId));

            if (role != null)
                return Task.FromResult(role);

            return null;
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = _unitOfWork.RoleRepository.FindByName(normalizedRoleName);

            return Task.FromResult(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.NormalizedRoleName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            role.NormalizedRoleName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var success = _unitOfWork.RoleRepository.Update(role);

            if (success)
                return Task.FromResult(IdentityResult.Success);

            return AddErrorMessage("Can not update role");
        }


        #region private method
        private Task<IdentityResult> AddErrorMessage(string message)
        {
            var identityError = new IdentityError
            {
                Description = message
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }
        #endregion
    }
}
