using SimpleStore.Entities;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Interfaces
{
    public interface IQueryRepositoryBase<T, ID> where T : EntityBase
    {
        Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TInfo>> selector,
            Expression<Func<T, TKey>> orderBy,
            int page,
            int rows);

        Task<T?> FindAsync(ID id);
    }
}
