using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.Services.Abstract;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(RoleModel model)
        {
            var role = new Role
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedRoleName = model.Name.ToUpper()
            };

            _unitOfWork.RoleRepository.Add(role);
        }

        public void Delete(int id)
        {
            var role = _unitOfWork.RoleRepository.Get(id);

            if (role != null)
            {
                _unitOfWork.RoleRepository.Delete(id);
            }            
        }

        public RoleModel Get(int id)
        {            
            var role = _unitOfWork.RoleRepository.Get(id);

            var model = new RoleModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return model;
        }

        public IList<RoleModel> GetAll()
        {
            var roles = _unitOfWork.RoleRepository.GetAll();

            var listOfRoleModel = new List<RoleModel>();

            foreach (var role in roles)
            {
                var model = new RoleModel
                {
                    Id = role.Id,
                    Name = role.Name
                };

                listOfRoleModel.Add(model);
            }

            return listOfRoleModel;
        }

        public void Update(RoleModel model)
        {
            var role = new Role
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedRoleName = model.Name.ToUpper()
            };

            _unitOfWork.RoleRepository.Update(role);
        }

        public bool IsExists(RoleModel model)
        {
            var roleName = _unitOfWork.RoleRepository.FindByName(model.Name);

            if (roleName == null)
            {
                return false;
            }

            return true;
        }
    }
}
