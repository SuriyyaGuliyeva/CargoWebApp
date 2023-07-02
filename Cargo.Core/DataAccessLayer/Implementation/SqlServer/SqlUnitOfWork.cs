using Cargo.Core.DataAccessLayer.Abstract;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ICountryRepository CountryRepository => new SqlCountryRepository(_connectionString);
        public IShopRepository ShopRepository => new SqlShopRepository(_connectionString);
        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(_connectionString);
        public IUserRepository UserRepository => new SqlUserRepository(_connectionString);
        public IRoleRepository RoleRepository => new SqlRoleRepository(_connectionString);
        public IUserRoleRepository UserRoleRepository => new SqlUserRoleRepository(_connectionString);

        public async Task<int> SaveAsync()
        {
            try
            {
                await Task.Run(() => _transaction.Commit());
                _transaction = _connection.BeginTransaction();
                return 0; // Or return any other relevant information about the save operation
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}
