using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.AdminPanel.Identity
{
    public class UserStore : IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>
    {
        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();                

                var normalizedRoleName = roleName.ToUpper();

                string query = "SELECT Id FROM roles WHERE NormalizedRoleName = @normalizedRoleName";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("normalizedRoleName", normalizedRoleName);

                int? roleId = Convert.ToInt32(cmd.ExecuteScalar());

                if (roleId == null)
                {
                    query = "INSERT INTO roles (Name, NormalizedName) output inserted.Id VALUES (@roleName, @normalizedRoleName)";

                    cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("roleName", roleName);
                    cmd.Parameters.AddWithValue("normalizedRoleName", normalizedRoleName);

                    roleId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string mainQuery = "IF NOT EXISTS(SELECT 1 FROM UserRoles] WHERE userId = @userId AND roleId = @roleId) INSERT INTO UserRoles(userId, roleId) VALUES(@userId, @roleId)";

                cmd = new SqlCommand(mainQuery, connection);

                cmd.Parameters.AddWithValue("userId", user.Id);
                cmd.Parameters.AddWithValue("roleId", roleId);

                cmd.ExecuteScalar();
            }

            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "insert into users (Name, NormalizedUserName, Surname, Email, PasswordHash, PhoneNumber) output inserted.Id values (@Name, @NormalizedUserName, @Surname, @Email, @PasswordHash, @PhoneNumber)";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Surname", user.Surname ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);


                cmd.ExecuteScalar();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "delete from users where id = @id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("id", user.Id);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Close();
            }
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select * from users where Id = @Id";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", Int32.Parse(userId));

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User();

                    user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    user.NormalizedUserName = reader.GetString(reader.GetOrdinal("NormalizedUserName"));
                    user.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                    user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                    return Task.FromResult(user);
                }

                return null;
            }
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                // connection.Open();

                connection.Open();

                string query = "select * from users where NormalizedUserName = @NormalizedUserName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("NormalizedUserName", normalizedUserName);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User();

                    user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    user.NormalizedUserName = reader.GetString(reader.GetOrdinal("NormalizedUserName"));
                    user.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                    user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                    return Task.FromResult(user);
                }

                return null;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "SELECT r.* FROM Roles r INNER JOIN UserRoles ur ON ur.roleId = r.Id WHERE ur.userId = @userId";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("userId", user.Id);

                var reader = cmd.ExecuteReader();

                IList<Role> roles = new List<Role>();

                while (reader.Read())
                {
                    Role role = new Role();

                    role.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    role.Name = reader.GetString(reader.GetOrdinal("Name"));
                    role.NormalizedRoleName = reader.GetString(reader.GetOrdinal("NormalizedRoleName"));

                    roles.Add(role);
                }

                IList<string> roleNames = roles.Select(x => x.Name).ToList();

                return Task.FromResult(roleNames);
            }
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "SELECT u.* FROM Users u INNER JOIN UserRoles ur ON ur.UserId = u.Id INNER JOIN Roles r ON r.Id = ur.RoleId WHERE r.NormalizedRoleName = @normalizedName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("normalizedRoleName", roleName.ToUpper());

                var reader = cmd.ExecuteReader();

                IList<User> users = new List<User>();

                while (reader.Read())
                {
                    User user = new User();

                    user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    user.NormalizedUserName = reader.GetString(reader.GetOrdinal("NormalizedUserName"));
                    user.Surname = reader.GetString(reader.GetOrdinal("Surname"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                    user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                    users.Add(user);
                }

                return Task.FromResult(users.ToList() as IList<User>);
            }
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "SELECT Id FROM Roles WHERE NormalizedRoleName = @normalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("normalizedRoleName", roleName.ToUpper());

                int? roleId = Convert.ToInt32(cmd.ExecuteScalar());

                if (roleId == default(int))
                    return Task.FromResult(false);

                string mainQuery = "SELECT COUNT(*) FROM UserRoles WHERE userId = @userId AND roleId = @roleId";

                cmd = new SqlCommand(mainQuery, connection);

                cmd.Parameters.AddWithValue("userId", user.Id);
                cmd.Parameters.AddWithValue("roleId", roleId);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return Task.FromResult(result > 0);
            }
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "SELECT Id FROM Roles WHERE NormalizedRoleName = @normalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("normalizedRoleName", roleName.ToUpper());

                int? roleId = Convert.ToInt32(cmd.ExecuteScalar());              
                
                if (roleId != null)
                {
                    string mainQuery = "DELETE FROM UserRoles WHERE userId = @userId AND roleId = @roleId";

                    cmd = new SqlCommand(mainQuery, connection);

                    cmd.Parameters.AddWithValue("userId", user.Id);
                    cmd.Parameters.AddWithValue("roleId", roleId);
                }

                return Task.CompletedTask;
            }
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Name = userName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                string query = "update users set Name = @Name, NormalizedUserName = @NormalizedUserName, Surname = @Surname, Email = @Email, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber where id = @Id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Id", user.Id);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Surname", user.Surname ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
