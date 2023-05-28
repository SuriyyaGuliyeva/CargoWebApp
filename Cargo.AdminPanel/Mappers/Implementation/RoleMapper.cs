using Cargo.AdminPanel.Mappers.Abstract;
using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Implementation
{
    public class RoleMapper : IRoleMapper
    {
        public RoleModel Map(Role role)
        {
            var model = new RoleModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return model;
        }

        public Role Map(RoleModel model)
        {
            var role = new Role
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedRoleName = model.Name.ToUpper()
            };

            return role;
        }
    }
}
