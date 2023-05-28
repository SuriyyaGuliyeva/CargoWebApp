using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
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

            _unitOfWork.UserRepository.AddToRoleAsync(user, roleName);

            return Task.CompletedTask;
        }

        public Task<bool> CheckPasswordAsync(SignInModel model, string password)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepository.CheckPasswordAsync(user, password).Result;

            return Task.FromResult(result);
        }

        public Task<IdentityResult> CreateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.CreateAsync(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.DeleteAsync(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.UserRepository.Dispose();
        }

        public Task<SignInModel> FindByIdAsync(string userId)
        {
            var user = _unitOfWork.UserRepository.FindByIdAsync(userId).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<SignInModel> FindByNameAsync(string normalizedUserName)
        {
            var user = _unitOfWork.UserRepository.FindByNameAsync(normalizedUserName).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<string> GetNormalizedUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var normalizedUserName = _unitOfWork.UserRepository.GetNormalizedUserNameAsync(user);

            return normalizedUserName;
        }

        public Task<string> GetPasswordHashAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var password = _unitOfWork.UserRepository.GetPasswordHashAsync(user);

            return password;
        }

        public Task<IList<string>> GetRolesAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var roles = _unitOfWork.UserRepository.GetRolesAsync(user);

            return roles;
        }

        public Task<string> GetUserIdAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var userId = _unitOfWork.UserRepository.GetUserIdAsync(user);

            return userId;
        }

        public Task<string> GetUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var username = _unitOfWork.UserRepository.GetUserNameAsync(user);

            return username;
        }

        public Task<IList<SignInModel>> GetUsersInRoleAsync(string roleName)
        {
            var users = _unitOfWork.UserRepository.GetUsersInRoleAsync(roleName).Result;

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

            var result = _unitOfWork.UserRepository.HasPasswordAsync(user).Result;

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepository.IsInRoleAsync(user, roleName).Result;

            return Task.FromResult(result);
        }

        public Task RemoveFromRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.RemoveFromRoleAsync(user, roleName);

            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(SignInModel model, string normalizedName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetNormalizedUserNameAsync(user, normalizedName);

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(SignInModel model, string passwordHash)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetPasswordHashAsync(user, passwordHash);

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(SignInModel model, string userName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SetUserNameAsync(user, userName);

            return Task.CompletedTask;
        }

        public Task SignInAsync(SignInModel model, bool isPersistent)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.SignInAsync(user, isPersistent);

            return Task.CompletedTask;
        }

        public Task SignOutAsync()
        {
            _unitOfWork.UserRepository.SignOutAsync();

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepository.UpdateAsync(user);

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
