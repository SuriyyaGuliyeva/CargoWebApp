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

        public void Add(Category t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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

        public void Update(Category t)
        {
            throw new NotImplementedException();
        }
    }
}
