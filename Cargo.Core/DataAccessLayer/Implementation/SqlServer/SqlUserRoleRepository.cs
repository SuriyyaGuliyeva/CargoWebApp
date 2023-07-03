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
        public int AddToRole(UserRole userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "insert into UserRoles (UserId, RoleId, IsDeleted) output inserted.Id values (@UserId, @RoleId, @IsDeleted)";

                connection.Open();

                var cmd = new SqlCommand(query, connection);

                AddParameters(cmd, userRole);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }            
        }

        public IList<string> GetRoles(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT r.* FROM Roles r INNER JOIN UserRoles ur ON ur.roleId = r.Id WHERE ur.userId = @userId";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("userId", userId);

                var reader = cmd.ExecuteReader();

                IList<Role> roles = new List<Role>();

                while (reader.Read())
                {
                    var role = GetFromReaderForRole(reader);

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

                string query = "SELECT u.* FROM Users u INNER JOIN UserRoles ur ON ur.UserId = u.Id INNER JOIN Roles r ON r.Id = ur.RoleId WHERE r.NormalizedRoleName = @normalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("normalizedRoleName", roleName.ToUpper());

                var reader = cmd.ExecuteReader();

                IList<User> users = new List<User>();

                while (reader.Read())
                {
                    var user = GetFromReaderForUser(reader);

                    users.Add(user);
                }

                return users.ToList();
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

        public void RemoveFromRole(int userId, string roleName)
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

                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("roleId", roleId);
                }
            }
        }        

        public int Add(UserRole userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "insert into UserRoles (UserId, RoleId, IsDeleted) output inserted.Id values (@UserId, @RoleId, @IsDeleted)";

                connection.Open();

                var cmd = new SqlCommand(query, connection);

                AddParameters(cmd, userRole);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Update(UserRole userRole)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update UserRoles set userId = @userId, roleId = @roleId where id = @Id and IsDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Id", userRole.Id);

                AddParameters(cmd, userRole);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
            }
        }

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update UserRoles set isDeleted = 1 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
            }
        }

        public IList<UserRole> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from UserRoles where isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                IList<UserRole> userRoles = new List<UserRole>();

                while (reader.Read())
                {
                    var userRole = GetFromReader(reader);

                    userRoles.Add(userRole);
                }

                return userRoles;
            }
        }

        public UserRole Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from UserRoles where id = @id and isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }


        #region private methods
        // for Role Entity     
        private Role GetFromReaderForRole(SqlDataReader reader)
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

        // For UserRole
        private void AddParameters(SqlCommand cmd, UserRole userRole)
        {
            cmd.Parameters.AddWithValue("Name", userRole.UserId);
            cmd.Parameters.AddWithValue("CreationDateTime", userRole.RoleId);
            cmd.Parameters.AddWithValue("IsDeleted", userRole.IsDeleted);
        }

        private UserRole GetFromReader(SqlDataReader reader)
        {
            UserRole userRole = new UserRole
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            };

            return userRole;
        }
        #endregion
    }
}
