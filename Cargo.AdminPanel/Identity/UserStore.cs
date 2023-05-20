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
    public class UserStore : IUserStore<User>, IUserRoleStore<User>
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
                connection.OpenAsync(cancellationToken);

                var normalizedRoleName = roleName.ToUpper();

                string query = "SELECT Id FROM roles WHERE NormalizedRoleName = @normalizedRoleName and isDeleted = 0";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("normalizedRoleName", normalizedRoleName);

                int? roleId = cmd.ExecuteNonQuery();

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
                connection.OpenAsync(cancellationToken);

                string query = "insert into users (Name, NormalizedUserName, Surname, Email, Password, PhoneNumber) output inserted.Id values (@Name, @NormalizedUserName, @Surname, @Email, @Password, @PhoneNumber)";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName);
                cmd.Parameters.AddWithValue("Surname", user.Surname ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Password", user.Password);
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
                connection.OpenAsync(cancellationToken);

                string query = "update users set isDeleted = 1 where id = @id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("id", user.Id);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {            
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.OpenAsync(cancellationToken);

                string query = "select * from users where isDeleted = 0 and Id = @Id";

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
                    user.Password = reader.GetString(reader.GetOrdinal("Password"));                    
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
                connection.OpenAsync(cancellationToken);

                string query = "select * from users where isDeleted = 0 and NormalizedUserName = @NormalizedUserName";

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
                    user.Password = reader.GetString(reader.GetOrdinal("Password"));
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

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.OpenAsync(cancellationToken);

                string query = "SELECT r.Name FROM Roles r INNER JOIN UserRoles ur ON ur.roleId = r.Id WHERE ur.userId = @userId";

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
           
                return Task.FromResult(roles.ToList() as IList<string>);
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
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

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
                connection.OpenAsync(cancellationToken);

                string query = "update users set Name = @Name, NormalizedUserName = @NormalizedUserName, Surname = @Surname, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber where id = @Id and IsDeleted = 0";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Id", user.Id);
                cmd.Parameters.AddWithValue("Name", user.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName);
                cmd.Parameters.AddWithValue("Surname", user.Surname ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();            
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
