using Cargo.Core.DataAccessLayer.Abstract;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;

        //public SqlUnitOfWork()
        //{
        //}

        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICountryRepository CountryRepository => new SqlCountryRepository(_connectionString);
        
        public IShopRepository ShopRepository => new SqlShopRepository(_connectionString);
    }
}
