using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
            var id = _unitOfWork.RoleRepository.Add(role);

            cancellationToken.ThrowIfCancellationRequested();

            if (id > 0)
                return Task.FromResult(IdentityResult.Success);

            var identityError = new IdentityError
            {
                Description = "Can not add role"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }
        
        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var success = _unitOfWork.RoleRepository.Delete(role.Id);

            cancellationToken.ThrowIfCancellationRequested();

            if (success)
                return Task.FromResult(IdentityResult.Success);

            var identityError = new IdentityError
            {
                Description = "Can not delete role"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public void Dispose()
        {
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return _unitOfWork.RoleRepository.FindByIdAsync(roleId, cancellationToken);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _unitOfWork.RoleRepository.FindByNameAsync(normalizedRoleName, cancellationToken);         
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

            _unitOfWork.RoleRepository.Update(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task UpdateAsync(Role role)
        {
            throw new System.NotImplementedException();
        }
    }
}
