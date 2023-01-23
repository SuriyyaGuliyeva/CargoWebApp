using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlCountryRepository : ICountryRepository
    {
        private readonly string _connectionString;

        public SqlCountryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Country country)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into countries (name, creationDateTime, isDeleted) values(@name, @creationDateTime, 0)";

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
                string query = "update countries set isDeleted = 1 where id = @id";

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
                string query = "select * from countries where id = @id and isDeleted = 0";

                connection.Open();

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                Country country = null;

                if (reader.Read())
                {
                    country = new Country();

                    country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    country.Name = reader.GetString(reader.GetOrdinal("Name"));
                    country.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    country.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));                              
                }

                return country;
            }
        }

        public IList<Country> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from countries where isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                List<Country> countries = new List<Country>();

                while (reader.Read())
                {
                    Country country = new Country();

                    country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    country.Name = reader.GetString(reader.GetOrdinal("Name"));
                    country.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
                    country.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                    countries.Add(country);
                }

                return countries;
            }
        }

        public void Update(Country country)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update countries set name = @name, creationDateTime = @creationDateTime where id = @id and isDeleted = 0";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("id", country.Id);
                cmd.Parameters.AddWithValue("name", country.Name);
                cmd.Parameters.AddWithValue("creationDateTime", country.CreationDateTime);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // do something
                }
            }
        }
    }
}
