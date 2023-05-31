using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUserRepositoryTest : IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>
    {
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
