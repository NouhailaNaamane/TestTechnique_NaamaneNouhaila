using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
    }
}
