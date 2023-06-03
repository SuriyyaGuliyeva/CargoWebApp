using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _userMapper;

        public UserService(IUnitOfWork unitOfWork, IUserMapper userMapper)
        {
            _unitOfWork = unitOfWork;
            _userMapper = userMapper;
        }

        public Task AddToRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.AddToRoleAsync(user, roleName, CancellationToken.None);

            return Task.CompletedTask;
        }      

        public Task<IdentityResult> CreateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.CreateAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.DeleteAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.UserRepository.Dispose();
        }

        public Task<SignInModel> FindByIdAsync(string userId)
        {
            var user = _unitOfWork.UserRepository.FindByIdAsync(userId, CancellationToken.None).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<SignInModel> FindByNameAsync(string normalizedUserName)
        {
            var user = _unitOfWork.UserRepository.FindByNameAsync(normalizedUserName, CancellationToken.None).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<string> GetNormalizedUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var normalizedUserName = _unitOfWork.UserRepository.GetNormalizedUserNameAsync(user, CancellationToken.None);

            return normalizedUserName;
        }

        public Task<string> GetPasswordHashAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var password = _unitOfWork.UserRepository.GetPasswordHashAsync(user, CancellationToken.None);

            return password;
        }

        public Task<IList<string>> GetRolesAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var roles = _unitOfWork.UserRepository.GetRolesAsync(user, CancellationToken.None);

            return roles;
        }

        public Task<string> GetUserIdAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var userId = _unitOfWork.UserRepository.GetUserIdAsync(user, CancellationToken.None);

            return userId;
        }

        public Task<string> GetUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var username = _unitOfWork.UserRepository.GetUserNameAsync(user, CancellationToken.None);

            return username;
        }

        public Task<IList<SignInModel>> GetUsersInRoleAsync(string roleName)
        {
            var users = _unitOfWork.UserRepository.GetUsersInRoleAsync(roleName, CancellationToken.None).Result;

            IList<SignInModel> models = null;

            foreach(var user in users)
            {
                var model = _userMapper.Map(user);

                models.Add(model);
            }

            return Task.FromResult(models);
        }

        public Task<bool> HasPasswordAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepository.HasPasswordAsync(user, CancellationToken.None).Result;

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepository.IsInRoleAsync(user, roleName, CancellationToken.None).Result;

            return Task.FromResult(result);
        }

        public Task RemoveFromRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.RemoveFromRoleAsync(user, roleName, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(SignInModel model, string normalizedName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetNormalizedUserNameAsync(user, normalizedName, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(SignInModel model, string passwordHash)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetPasswordHashAsync(user, passwordHash, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(SignInModel model, string userName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetUserNameAsync(user, userName, CancellationToken.None);

            return Task.CompletedTask;
        }     

        public Task<IdentityResult> UpdateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.UpdateAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
