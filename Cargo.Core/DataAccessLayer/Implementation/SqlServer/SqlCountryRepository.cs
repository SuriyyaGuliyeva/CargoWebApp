using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
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

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update countries set isDeleted = 1 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;

                return true;
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

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        public IList<Country> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select * from countries where isDeleted = 0 order by name";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                IList<Country> countries = new List<Country>();

                while (reader.Read())
                {
                    var country = GetFromReader(reader);

                    countries.Add(country);
                }

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
                AddParameters(cmd, country);

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // do something
                }
            }
        }

        public Country GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "select * from countries where name = @name and isDeleted = 0";

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

        public int Add(Country country)
        {
            int insertedId = 0;

            using (var con = new SqlConnection(_connectionString))
            {
                string query = "insert into countries (name, creationDateTime, isDeleted) values(@name, @creationDateTime, @isDeleted); SELECT SCOPE_IDENTITY();";

                con.Open();

                var cmd = new SqlCommand(query, con);

                AddParameters(cmd, country);

                insertedId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return insertedId;
        }

        #region private methods

        private void AddParameters(SqlCommand cmd, Country country)
        {
            cmd.Parameters.AddWithValue("Name", country.Name);
            cmd.Parameters.AddWithValue("CreationDateTime", country.CreationDateTime);
            cmd.Parameters.AddWithValue("IsDeleted", country.IsDeleted);
        }

        private Country GetFromReader(SqlDataReader reader)
        {
            Country country = new Country();

            country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            country.Name = reader.GetString(reader.GetOrdinal("Name"));
            country.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));
            country.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

            return country;
        }
        #endregion
    }
}
