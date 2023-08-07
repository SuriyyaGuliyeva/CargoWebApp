using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public SqlCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Category category)
        {
            int insertedId = 0;

            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into categories (name, creationDateTime, isDeleted) values(@name, @creationDateTime, @isDeleted); SELECT SCOPE_IDENTITY();";

                con.Open();

                var cmd = new SqlCommand(query, con);

                AddParameters(cmd, category);

                insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return insertedId;
        }

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update categories set isDeleted = 1 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
            }
        }

        public Category Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from categories where id = @id and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                Category category = null;

                if (reader.Read())
                {
                    category = new Category();

                    category.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    category.Name = reader.GetString(reader.GetOrdinal("Name"));
                    category.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    category.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
                }

                return category;
            }
        }

        public IList<Category> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from categories where isDeleted = 0 order by name";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                IList<Category> categories = new List<Category>();

                while (reader.Read())
                {
                    Category category = new Category();

                    category.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    category.Name = reader.GetString(reader.GetOrdinal("Name"));
                    category.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    category.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                    categories.Add(category);
                }

                return categories;
            }
        }

        public bool Update(Category category)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update categories set name = @name, creationDateTime = @creationDateTime where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("id", category.Id);
                AddParameters(cmd, category);

                var affectedRows = cmd.ExecuteNonQuery();

                return affectedRows > 0;
            }
        }

        public Category GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from categories where name = @name and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("name", name);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        #region private methods
        private void AddParameters(SqlCommand cmd, Category category)
        {
            cmd.Parameters.AddWithValue("Name", category.Name);
            cmd.Parameters.AddWithValue("CreationDateTime", category.CreationDateTime);
            cmd.Parameters.AddWithValue("IsDeleted", category.IsDeleted);
        }

        private Category GetFromReader(SqlDataReader reader)
        {
            Category category = new Category();

            category.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            category.Name = reader.GetString(reader.GetOrdinal("Name"));
            category.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
            category.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

            return category;
        }
        #endregion
    }
}
