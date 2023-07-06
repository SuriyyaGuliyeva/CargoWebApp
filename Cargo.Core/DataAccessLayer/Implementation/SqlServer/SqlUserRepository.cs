using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "insert into users (Name, NormalizedUserName, Surname, Email, PasswordHash, PhoneNumber, IsDeleted) output inserted.Id values (@Name, @NormalizedUserName, @Surname, @Email, @PasswordHash, @PhoneNumber, @IsDeleted)";

                var cmd = new SqlCommand(query, connection);

                AddParameters(cmd, user);

                return (int)cmd.ExecuteScalar();
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update users set IsDeleted = 1 where id = @id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update users set Name = @Name, NormalizedUserName = @NormalizedUserName, Surname = @Surname, Email = @Email, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber where id = @Id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Id", user.Id);

                AddParameters(cmd, user);

                var affectedRows = cmd.ExecuteNonQuery();

                return affectedRows > 0;
            }
        }
        
        public User FindByName(string normalizedUserName)
        {
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

        public User Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from users where id = @id and isDeleted = 0";

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

        public IList<User> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from users where isDeleted = 0 order by name";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                IList<User> users = new List<User>();

                while (reader.Read())
                {
                    var user = GetFromReader(reader);

                    users.Add(user);
                }

                return users;
            }
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

        private User GetFromReader(SqlDataReader reader)
        {
            User user = new User
            {
                //.GetValue("Photo").ToString()
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
