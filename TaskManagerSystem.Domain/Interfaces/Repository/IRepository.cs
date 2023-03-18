using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id, string includeProperties = "");
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> Insert(T item);
        Task<IEnumerable<T>> Insert(IEnumerable<T> items);
        Task<T> Update(T item);
        Task<bool> Delete(long id);
        Task<bool> Delete(IEnumerable<T> items);
    }
}
