using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlShopRepository : IShopRepository
    {
        private readonly string _connectionString;

        public SqlShopRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Shop shop)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into shops (Name, CreationDateTime, Link, Photo, IsDeleted, CountryId, CategoryId) output inserted.Id values (@Name, @CreationDateTime, @Link, @Photo, @IsDeleted, @CountryId, @CategoryId)";

                con.Open();

                var cmd = new SqlCommand(query, con);

                AddParameters(cmd, shop);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update shops set isDeleted = 1 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
            }
        }

        public Shop Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"select
                    sh.*,
                    co.Name as CountryName,
                    cat.Name as CategoryName
                    from shops as sh
                    join Countries as co
                    on sh.CountryId = co.Id
                    join Categories as cat
                    on sh.CategoryId = cat.Id
                    where sh.id = @id and sh.IsDeleted = 0";

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

        public bool Update(Shop shop)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update shops set name = @name, link = @link, photo = @photo, countryId = @countryId, categoryId = @categoryId where id = @Id and IsDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Id", shop.Id);

                AddParameters(cmd, shop);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
            }
        }

        public Shop GetByCategoryId(string name, int categoryId, int countryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where name = @name and CategoryId = @categoryId and CountryId = @countryId and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                cmd.Parameters.AddWithValue("CountryId", countryId);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        public IList<Shop> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"select
                    sh.*,
                    co.Name as CountryName,
                    cat.Name as CategoryName
                    from shops as sh
                    join Countries as co
                    on sh.CountryId = co.Id
                    join Categories as cat
                    on sh.CategoryId = cat.Id
                    where sh.IsDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                List<Shop> shops = new List<Shop>();

                while (reader.Read())
                {
                    var shop = GetFromReader(reader);

                    shops.Add(shop);
                }

                return shops;
            }
        }

        public int GetTotalCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select count(*) from shops where isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
        }

        #region private methods

        private void AddParameters(SqlCommand cmd, Shop shop)
        {
            cmd.Parameters.AddWithValue("Name", shop.Name);
            cmd.Parameters.AddWithValue("CreationDateTime", shop.CreationDateTime);
            cmd.Parameters.AddWithValue("Link", shop.Link ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("Photo", shop.Photo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("IsDeleted", shop.IsDeleted);
            cmd.Parameters.AddWithValue("CountryId", shop.CountryId);
            cmd.Parameters.AddWithValue("CategoryId", shop.CategoryId);
        }

        private Shop GetFromReader(SqlDataReader reader)
        {
            Shop shop = new Shop();

            shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            shop.Name = reader.GetString(reader.GetOrdinal("Name"));
            shop.Link = reader.GetString(reader.GetOrdinal("Link"));         
            shop.Photo = reader.GetValue("Photo").ToString();            
            shop.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
            shop.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));

            shop.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
            shop.Country = new Country()
            {
                Id = shop.CountryId,
                Name = reader.GetString(reader.GetOrdinal("CountryName"))
            };

            shop.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
            shop.Category = new Category()
            {
                Id = shop.CategoryId,
                Name = reader.GetString(reader.GetOrdinal("CategoryName"))
            };

            return shop;
        }
        #endregion
    }
}
