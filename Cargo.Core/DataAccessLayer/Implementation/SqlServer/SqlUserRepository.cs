using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User FindById(string userId)
        {
            throw new NotImplementedException();
        }

        public User FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
