namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IUnitOfWork
    {        
        public ICountryRepository CountryRepository { get; }
        public IShopRepository ShopRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
    }
}
