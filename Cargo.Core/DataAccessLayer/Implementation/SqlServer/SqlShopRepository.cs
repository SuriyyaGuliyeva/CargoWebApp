using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System.Collections.Generic;
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

        public void Add(Shop shop)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into shops (Name, CreationDateTime, Link, IsDeleted, Photo, CountryId, CategoryId) values (@Name, @CreationDateTime, @Link, @IsDeleted, @Photo, @CountryId, @CategoryId)";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Name", shop.Name);
                cmd.Parameters.AddWithValue("CreationDateTime", shop.CreationDateTime);
                cmd.Parameters.AddWithValue("Link", shop.Link);
                cmd.Parameters.AddWithValue("Photo", shop.Photo);
                cmd.Parameters.AddWithValue("IsDeleted", shop.IsDeleted);
                cmd.Parameters.AddWithValue("CountryId", shop.CountryId);
                cmd.Parameters.AddWithValue("CategoryId", shop.CategoryId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update shops set isDeleted = 1 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // do something
                }
            }
        }

        public Shop Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where id = @id and isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                Shop shop = null;

                if (reader.Read())
                {
                    shop = new();

                    shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    shop.Name = reader.GetString(reader.GetOrdinal("Name"));
                    shop.Link = reader.GetString(reader.GetOrdinal("Link"));
                    shop.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
                    shop.Photo = reader.GetString(reader.GetOrdinal("Photo"));
                    shop.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    shop.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                    shop.Country = new Country()
                    {
                        Id = shop.CountryId
                    };
                    shop.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                    shop.Category = new Category()
                    {
                        Id = shop.CategoryId
                    };
                }

                return shop;
            }
        }

        public IList<Shop> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                List<Shop> shops = new List<Shop>();

                while (reader.Read())
                {
                    Shop shop = new Shop();

                    shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    shop.Name = reader.GetString(reader.GetOrdinal("Name"));
                    shop.Link = reader.GetString(reader.GetOrdinal("Link"));
                    shop.Photo = reader.GetString(reader.GetOrdinal("Photo"));
                    shop.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
                    shop.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    shop.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                    shop.Country = new Country()
                    {
                        Id = shop.CountryId
                    };
                    shop.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                    shop.Category = new Category()
                    {
                        Id = shop.CategoryId
                    };

                    shops.Add(shop);
                }

                return shops;
            }
        }

        public void Update(Shop shop)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update shops set name = @name, link = @link, photo = @photo, creationDateTime = @creationDateTime, countryId = @countryId, categoryId = @categoryId where id = @id and IsDeleted = 0";
                
                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("id", shop.Id);
                cmd.Parameters.AddWithValue("name", shop.Name);
                cmd.Parameters.AddWithValue("link", shop.Link);
                cmd.Parameters.AddWithValue("photo", shop.Photo);
                cmd.Parameters.AddWithValue("creationDateTime", shop.CreationDateTime);
                cmd.Parameters.AddWithValue("countryId", shop.CountryId);
                cmd.Parameters.AddWithValue("categoryId", shop.CategoryId);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // do something
                }
            }
        }

        public string GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where name = @name and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("name", name);

                var reader = cmd.ExecuteReader();

                Shop shop = null;

                if (reader.Read())
                {
                    shop = new Shop();

                    shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    shop.Name = reader.GetString(reader.GetOrdinal("Name"));                  
                }

                if (shop == null)
                    return string.Empty;

                return shop.Name;
            }
        }

        public int GetByCategoryId(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where name = @name and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("name", name);

                var reader = cmd.ExecuteReader();

                Shop shop = null;

                if (reader.Read())
                {
                    shop = new Shop();

                    shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    shop.Name = reader.GetString(reader.GetOrdinal("Name"));
                    shop.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                    shop.Category = new Category()
                    {
                        Id = shop.CategoryId
                    };
                }

                if (shop == null)
                    return 0;

                return shop.CategoryId;
            }
        }
    }
}
