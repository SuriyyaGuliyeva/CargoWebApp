using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        int Add(T t);
        void Update(T t);
        bool Delete(int id);
        IList<T> GetAll();
        T Get(int id);
    }
}
