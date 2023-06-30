using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.DataAccessLayer.Implementation.SqlServer;
using Cargo.Core.Domain.Enums;

namespace Cargo.Core.Factories
{
    public static class DbFactory
    {       
        public static IUnitOfWork Create(VendorTypes type, string connectionString)
        {
            IUnitOfWork unitOfWork = null;

            switch (type)
            {
                case VendorTypes.SqlServer:                   
                    unitOfWork = new SqlUnitOfWork(connectionString);
                    break;
                case VendorTypes.Oracle:
                    break;
                default:
                    break;
            }

            return unitOfWork;
        }     
    }
}
