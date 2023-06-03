using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
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

                string query = "insert into users (Name, NormalizedUserName, Surname, Email, PasswordHash, PhoneNumber, IsDeleted) output inserted.Id values (@Name, @NormalizedUserName, @Surname, @Email, @PasswordHash, @PhoneNumber, @IsDeleted)";

                var cmd = new SqlCommand(query, connection);

                AddParameters(cmd, user);

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

                string query = "update users set IsDeleted = 1 where id = @id";

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
                    return GetFromReader(reader);                    
                }

                return null;
            }
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select * from users where NormalizedUserName = @NormalizedUserName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("NormalizedUserName", normalizedUserName);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
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
                    var user = GetFromReader(reader).Result;

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
                    string mainQuery = "update UserRoles set IsDeleted = 1 WHERE userId = @userId AND roleId = @roleId";

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

                AddParameters(cmd, user);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        #region private methods

        private void AddParameters(SqlCommand cmd, User user)
        {
            cmd.Parameters.AddWithValue("Name", user.Name);
            cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("Surname", user.Surname ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("IsDeleted", user.IsDeleted);
        }

        private Task<User> GetFromReader(SqlDataReader reader)
        {
            User user = new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                NormalizedUserName = reader.GetString(reader.GetOrdinal("NormalizedUserName")),
                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            };

            return Task.FromResult(user);
        }
        #endregion
    }
}
