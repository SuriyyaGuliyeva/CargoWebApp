using Cargo.AdminPanel.Models;
using Cargo.Core.Domain.Entities;

namespace Cargo.AdminPanel.Mappers.Abstract
{
    public interface IRoleMapper
    {
        RoleModel Map(Role role);

        Role Map(RoleModel model);
    }
}
