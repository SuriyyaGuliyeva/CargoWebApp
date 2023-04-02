﻿using Cargo.Core.DataAccessLayer.Abstract;
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
                string query = "insert into shops (Name, CreationDateTime, Link, IsDeleted, Photo, ImageHashCode, CountryId, CategoryId) values (@Name, @CreationDateTime, @Link, @IsDeleted, @Photo, @ImageHashCode, @CountryId, @CategoryId)";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Name", shop.Name);
                cmd.Parameters.AddWithValue("CreationDateTime", shop.CreationDateTime);
                cmd.Parameters.AddWithValue("Link", shop.Link);
                cmd.Parameters.AddWithValue("Photo", shop.Photo);
                cmd.Parameters.AddWithValue("ImageHashCode", shop.ImageHashCode);
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
                string query = "select * from shops where isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                List<Shop> shops = new List<Shop>();               

                while (reader.Read())
                {
                    var shop = GetFromReader(reader);

                    //Shop shop = new Shop();

                    //shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    //shop.Name = reader.GetString(reader.GetOrdinal("Name"));
                    //shop.Link = reader.GetString(reader.GetOrdinal("Link"));
                    //shop.Photo = reader.GetString(reader.GetOrdinal("Photo"));
                    //shop.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
                    //shop.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    //shop.CountryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
                    //shop.Country = new Country()
                    //{
                    //    Id = shop.CountryId
                    //};
                    //shop.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                    //shop.Category = new Category()
                    //{
                    //    Id = shop.CategoryId
                    //};                    

                    shops.Add(shop);
                }

                return shops;
            }
        }

        public void Update(Shop shop)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update shops set name = @name, link = @link, photo = @photo, imagehashcode = @imagehashcode, creationDateTime = @creationDateTime, countryId = @countryId, categoryId = @categoryId where id = @Id and IsDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Id", shop.Id);
                cmd.Parameters.AddWithValue("Name", shop.Name);
                cmd.Parameters.AddWithValue("Link", shop.Link);
                cmd.Parameters.AddWithValue("Photo", shop.Photo);
                cmd.Parameters.AddWithValue("ImageHashCode", shop.ImageHashCode);
                cmd.Parameters.AddWithValue("CreationDateTime", shop.CreationDateTime);
                cmd.Parameters.AddWithValue("IsDeleted", shop.IsDeleted);
                cmd.Parameters.AddWithValue("CountryId", shop.CountryId);
                cmd.Parameters.AddWithValue("CategoryId", shop.CategoryId);                

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // do something
                }
            }
        }

        public Shop GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from shops where name = @name and isDeleted = 0";

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

        public IList<Shop> GetAllWithJoinQuery()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select " +
                    "sh.*," +
                    "co.Name as CountryName," +
                    "cat.Name as CategoryName " +
                    "from shops as sh " +
                    "join Countries as co " +
                    "on sh.CountryId = co.Id " +
                    "join Categories as cat " +
                    "on sh.CategoryId = cat.Id " +
                    "where sh.IsDeleted = 0";

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

        private Shop GetFromReader(SqlDataReader reader)
        {
            Shop shop = new Shop();

            shop.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            shop.Name = reader.GetString(reader.GetOrdinal("Name"));
            shop.Link = reader.GetString(reader.GetOrdinal("Link"));
            shop.Photo = reader.GetString(reader.GetOrdinal("Photo"));
            shop.ImageHashCode = reader.GetString(reader.GetOrdinal("ImageHashCode"));
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

            return shop;
        }      
    }
}
