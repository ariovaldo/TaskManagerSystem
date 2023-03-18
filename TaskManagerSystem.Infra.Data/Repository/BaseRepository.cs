using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Infra.Data.Context;

namespace TaskManagerSystem.Infra.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        
        protected readonly TaskManagerSystemContext _context;
        protected readonly DbSet<T> _dataset;

        public BaseRepository(TaskManagerSystemContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            var items = await _dataset.ToListAsync();
            return items;
        }

        public virtual async Task<T> GetById(long id, string includeProperties = "")
        {
            var item = (T)null;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                IQueryable<T> query = _dataset;
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }

                item = await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            else
                item = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));

            return item;
        }
        
        public async virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dataset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> Insert(T item)
        {
            await _dataset.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<T>> Insert(IEnumerable<T> items)
        {
            await _dataset.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            return items;
        }

        public async Task<T> Update(T item)
        {
            var result = await _dataset.FindAsync(item.Id);

            if (result is null)
                return null;

            _dataset.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> Delete(long id)
        {
            var item = await _dataset.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (item is null)
                return false;

            _dataset.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(IEnumerable<T> items)
        {
            _dataset.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
