using SimpleStore.Entities;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> predicate);

        Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TInfo>> selector,
            Expression<Func<T, TKey>> orderBy,
            int page,
            int rows);

        Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TInfo>> selector);

        Task<T?> FindAsync(int id);

        Task<int> AddAsync(T t);

        Task UpdateAsync();

        Task DeleteAsync(int id);
    }
}
