using Cargo.Core.DataAccessLayer.Abstract;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;

        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;      
        }

        public ICountryRepository CountryRepository => new SqlCountryRepository(_connectionString);
        
        public IShopRepository ShopRepository => new SqlShopRepository(_connectionString);

        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(_connectionString);

        public IUserRepository UserRepository => new SqlUserRepository(_connectionString);

        public IRoleRepository RoleRepository => new SqlRoleRepository(_connectionString);
    }
}
