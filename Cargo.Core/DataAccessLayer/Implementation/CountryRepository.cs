using Cargo.Core.Config;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _connectionString = AppConfig.GetConnectionString();

        public void Add(Country country)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into countries (name, creationDateTime) values(@name, @creationDateTime)";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("name", country.Name);
                cmd.Parameters.AddWithValue("creationDateTime", country.CreationDateTime);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "delete from countries where id = @id";

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

        public Country Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from countries where id = @id";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                Country country = null;

                while (reader.Read())
                {
                    country = new Country();

                    country.Id = (int)reader["Id"];
                    country.Name = (string)reader["Name"];
                    country.CreationDateTime = (DateTime)reader["CreationDateTime"];                 
                }

                return country;
            }
        }

        public IList<Country> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from countries";

                con.Open();

                var cmd = new SqlCommand(query, con);
                var reader = cmd.ExecuteReader();

                var countries = Mapper.DataReaderMapToList<Country>(reader);

                return countries;
            }
        }

        public void Update(Country country)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update countries set name = @name, creationDateTime = @creationDateTime where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("id", country.Id);
                cmd.Parameters.AddWithValue("name", country.Name);
                cmd.Parameters.AddWithValue("creationDateTime", country.CreationDateTime);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
