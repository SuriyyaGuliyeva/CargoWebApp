using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category GetByName(string name);
    }
}
