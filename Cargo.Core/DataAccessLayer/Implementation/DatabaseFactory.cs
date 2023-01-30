﻿using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.DataAccessLayer.Implementation.SqlServer;
using Cargo.Core.Domain.Enums;
using System;

namespace Cargo.Core.DataAccessLayer.Implementation
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public IUnitOfWork DbFactory(DbName name)
        {
            IUnitOfWork unitOfWork = null;

            switch (name)
            {
                case DbName.SqlServer:
                    unitOfWork = new SqlUnitOfWork();
                    break;
                case DbName.Oracle:
                    break;
                default:
                    break;
            }

            return unitOfWork;
        }
    }
}