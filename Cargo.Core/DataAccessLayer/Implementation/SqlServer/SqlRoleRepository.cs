using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlRoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public SqlRoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "insert into roles (Name, NormalizedRoleName, IsDeleted) output inserted.Id values (@Name, @NormalizedRoleName, @IsDeleted)";

                var cmd = new SqlCommand(query, connection);

                AddParameters(cmd, role);

                return (int)cmd.ExecuteScalar();
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update roles set isDeleted = 1 where id = @id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Role FindByName(string normalizedRoleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select * from roles where NormalizedRoleName = @NormalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("NormalizedRoleName", normalizedRoleName);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        public Role Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from roles where id = @id and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        public IList<Role> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from roles where isDeleted = 0 order by name";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                IList<Role> roles = new List<Role>();

                while (reader.Read())
                {
                    var role = GetFromReader(reader);

                    roles.Add(role);
                }

                return roles;
            }
        }

        public bool Update(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update roles set Name = @Name, NormalizedRoleName = @NormalizedRoleName where id = @Id";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", role.Id);

                AddParameters(cmd, role);

                var affectedRows = cmd.ExecuteNonQuery();

                return affectedRows > 0;
            }
        }      


        #region private methods
        private void AddParameters(SqlCommand cmd, Role role)
        {
            cmd.Parameters.AddWithValue("Name", role.Name);
            cmd.Parameters.AddWithValue("NormalizedRoleName", role.NormalizedRoleName ?? (object)DBNull.Value);           
            cmd.Parameters.AddWithValue("IsDeleted", role.IsDeleted);
        }

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
        #endregion
    }
}
