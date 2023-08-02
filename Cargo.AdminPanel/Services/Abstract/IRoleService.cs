using Cargo.AdminPanel.Models;
using Cargo.AdminPanel.ViewModels;
using System.Collections.Generic;

namespace Cargo.AdminPanel.Services.Abstract
{
    public interface IRoleService
    {
        void Add(RoleModel model);
        void Update(RoleModel model);
        void Delete(int id);
        IList<RoleModel> GetAll();
        RoleModel Get(int id);
        bool IsExists(RoleModel model);
    }
}
