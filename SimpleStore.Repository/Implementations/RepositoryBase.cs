using Microsoft.EntityFrameworkCore;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly DbContext _context;

        protected RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> AddAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();

            return t.Id;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await FindAsync(id);
            if(entity != null) throw new InvalidOperationException($"No se encontró el registro con el Id {id}");
            entity!.Status = false;
            await UpdateAsync();
        }

        public virtual async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T?> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TInfo>> selector, Expression<Func<T, TKey>> orderBy, int page, int rows)
        {
            var collection = await _context.Set<T>()
                .Where(predicate)
                .OrderBy(orderBy)
                .Skip((page - 1) * rows)
                .Take(rows)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();

            var total_rows = await _context.Set<T>()
                .Where(predicate)
                .CountAsync();

            return (collection, total_rows);
        }

        public async Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<T, bool>> predicate, Expression<Func<T, TInfo>> selector)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
        }
    }
}
