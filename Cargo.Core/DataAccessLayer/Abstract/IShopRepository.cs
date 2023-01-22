using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IShopRepository
    {
        void Add(Shop shop);
        void Update(Shop shop);
        void Delete(int id);
        IList<Shop> GetAll();
        Shop Get(int id);        
    }
}
