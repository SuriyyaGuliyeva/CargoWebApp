using System.Collections.Generic;

namespace Cargo.Core.DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        void Delete(int id);
        IList<T> GetAll();
        T Get(int id);
    }
}
