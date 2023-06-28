using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlRoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public SqlRoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Role t)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "insert into roles (Name, NormalizedRoleName) output inserted.Id values (@Name, @NormalizedRoleName)";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Name", role.Name);
                cmd.Parameters.AddWithValue("NormalizedRoleName", role.NormalizedRoleName);

                cmd.ExecuteScalar();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update roles set isDeleted = 1 where id = @id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("id", role.Id);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Close();
            }
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select * from roles where Id = @Id";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", Int32.Parse(roleId));

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Role role = new Role();

                    role.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    role.Name = reader.GetString(reader.GetOrdinal("Name"));
                    role.NormalizedRoleName = reader.GetString(reader.GetOrdinal("NormalizedRoleName"));

                    return Task.FromResult(role);
                }

                return null;
            }
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "select * from roles where NormalizedRoleName = @NormalizedRoleName";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("NormalizedRoleName", normalizedRoleName);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Role role = new Role();

                    role.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    role.Name = reader.GetString(reader.GetOrdinal("Name"));
                    role.NormalizedRoleName = reader.GetString(reader.GetOrdinal("NormalizedRoleName"));

                    return Task.FromResult(role);
                }

                return null;
            }
        }

        public Role Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Role t)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "update roles set Name = @Name, NormalizedRoleName = @NormalizedRoleName where id = @Id";

                var cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("Id", role.Id);
                cmd.Parameters.AddWithValue("Name", role.Name);
                cmd.Parameters.AddWithValue("NormalizedRoleName", role.NormalizedRoleName);

                cmd.ExecuteNonQuery();
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
