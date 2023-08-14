using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IUserMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserModel Get(int id)
        {
            var user = _unitOfWork.UserRepository.Get(id);          

            var roles = _unitOfWork.UserRoleRepository.GetRoles(id);

            var model = _mapper.MapUser(user);

            foreach (var role in roles)
            {
                model.RoleName = model.RoleName is null ? role : $"{model.RoleName}, {role}";
            }

            return model;
        }

        public IList<UserModel> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();

            var listOfUserModel = new List<UserModel>();

            foreach (var user in users)
            {
                var model = _mapper.MapUser(user);               

                listOfUserModel.Add(model);
            }

            return listOfUserModel;
        }

        //public int GetTotalUserCount()
        //{
        //    return _unitOfWork.UserRepository.GetTotalCount();
        //}
    }
}
