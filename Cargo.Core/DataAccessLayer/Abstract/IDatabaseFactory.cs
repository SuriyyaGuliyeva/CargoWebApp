using Cargo.Core.Domain.Enums;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IDatabaseFactory
    {
        IUnitOfWork DbFactory(DbName name);
    }
}
