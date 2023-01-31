using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.DataAccessLayer.Implementation.SqlServer;
using Cargo.Core.Domain.Enums;
using System;

namespace Cargo.Core
{
    public static class Creator
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            set
            {
                if (_connectionString == null)
                {
                    _connectionString = value;
                }
                else
                {
                    throw new InvalidOperationException("Connection string can be set only once");
                }
            }
        }

        public static IUnitOfWork Create(VendorTypes type)
        {
            IUnitOfWork unitOfWork = null;

            switch (type)
            {
                case VendorTypes.SqlServer:
                    unitOfWork = new SqlUnitOfWork(_connectionString);
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
