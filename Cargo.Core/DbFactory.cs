using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.DataAccessLayer.Implementation.SqlServer;
using Cargo.Core.Domain.Enums;
using System;

namespace Cargo.Core
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
