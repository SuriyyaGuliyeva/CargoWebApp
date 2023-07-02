using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUserRoleRepository : IUserRoleRepository
    {
        private readonly string _connectionString;

        public SqlUserRoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddToRole(User user, string roleName)
        {
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
        }

        public IList<string> GetRoles(User user)
        {
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
                    var role = GetFromReader(reader);

                    roles.Add(role);
                }

                IList<string> roleNames = roles.Select(x => x.Name).ToList();

                return roleNames;  
            }
        }

        public IList<User> GetUsersInRole(string roleName)
        {           
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
                    var user = GetFromReaderForUser(reader);

                    users.Add(user);
                }

                return users.ToList() as IList<User>;
            }
        }

        public bool IsInRole(User user, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id FROM Roles WHERE NormalizedRoleName = @normalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("normalizedRoleName", roleName.ToUpper());

                int? roleId = Convert.ToInt32(cmd.ExecuteScalar());

                if (roleId == default(int))
                    return false;

                string mainQuery = "SELECT COUNT(*) FROM UserRoles WHERE userId = @userId AND roleId = @roleId";

                cmd = new SqlCommand(mainQuery, connection);

                cmd.Parameters.AddWithValue("userId", user.Id);
                cmd.Parameters.AddWithValue("roleId", roleId);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return result > 0;
            }
        }

        public void RemoveFromRole(User user, string roleName)
        {            
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
            }
        }

        #region private methods
        // for Role Entity     
        private Role GetFromReader(SqlDataReader reader)
        {
            Role role = new Role
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                NormalizedRoleName = reader.GetString(reader.GetOrdinal("NormalizedRoleName")),               
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            };

            return role;
        }

        // for User Entity       
        private User GetFromReaderForUser(SqlDataReader reader)
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

            return user;
        }
        #endregion
    }
}
