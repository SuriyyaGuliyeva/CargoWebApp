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

            _unitOfWork.UserRepositoryTest.AddToRoleAsync(user, roleName, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task<bool> CheckPasswordAsync(SignInModel model, string password)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepositoryTest.CheckPasswordAsync(user, password).Result;

            return Task.FromResult(result);
        }

        public Task<IdentityResult> CreateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.CreateAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.DeleteAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.UserRepositoryTest.Dispose();
        }

        public Task<SignInModel> FindByIdAsync(string userId)
        {
            var user = _unitOfWork.UserRepositoryTest.FindByIdAsync(userId, CancellationToken.None).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<SignInModel> FindByNameAsync(string normalizedUserName)
        {
            var user = _unitOfWork.UserRepositoryTest.FindByNameAsync(normalizedUserName, CancellationToken.None).Result;

            var model = _userMapper.Map(user);

            return Task.FromResult(model);
        }

        public Task<string> GetNormalizedUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var normalizedUserName = _unitOfWork.UserRepositoryTest.GetNormalizedUserNameAsync(user, CancellationToken.None);

            return normalizedUserName;
        }

        public Task<string> GetPasswordHashAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var password = _unitOfWork.UserRepositoryTest.GetPasswordHashAsync(user, CancellationToken.None);

            return password;
        }

        public Task<IList<string>> GetRolesAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var roles = _unitOfWork.UserRepositoryTest.GetRolesAsync(user, CancellationToken.None);

            return roles;
        }

        public Task<string> GetUserIdAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var userId = _unitOfWork.UserRepositoryTest.GetUserIdAsync(user, CancellationToken.None);

            return userId;
        }

        public Task<string> GetUserNameAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            var username = _unitOfWork.UserRepositoryTest.GetUserNameAsync(user, CancellationToken.None);

            return username;
        }

        public Task<IList<SignInModel>> GetUsersInRoleAsync(string roleName)
        {
            var users = _unitOfWork.UserRepositoryTest.GetUsersInRoleAsync(roleName, CancellationToken.None).Result;

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

            var result = _unitOfWork.UserRepositoryTest.HasPasswordAsync(user, CancellationToken.None).Result;

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            var result = _unitOfWork.UserRepositoryTest.IsInRoleAsync(user, roleName, CancellationToken.None).Result;

            return Task.FromResult(result);
        }

        public Task RemoveFromRoleAsync(SignInModel model, string roleName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.RemoveFromRoleAsync(user, roleName, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(SignInModel model, string normalizedName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.SetNormalizedUserNameAsync(user, normalizedName, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(SignInModel model, string passwordHash)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.SetPasswordHashAsync(user, passwordHash, CancellationToken.None);

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(SignInModel model, string userName)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.SetUserNameAsync(user, userName, CancellationToken.None);

            return Task.CompletedTask;
        }

        //public Task SignInAsync(SignInModel model, bool isPersistent)
        //{
        //    var user = _userMapper.Map(model);

        //    _unitOfWork.UserRepositoryTest.SignInAsync(user, isPersistent);

        //    return Task.CompletedTask;
        //}

        //public Task SignOutAsync()
        //{
        //    _unitOfWork.UserRepositoryTest.SignOutAsync();

        //    return Task.CompletedTask;
        //}

        public Task<IdentityResult> UpdateAsync(SignInModel model)
        {
            var user = _userMapper.Map(model);

            _unitOfWork.UserRepositoryTest.UpdateAsync(user, CancellationToken.None);

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
