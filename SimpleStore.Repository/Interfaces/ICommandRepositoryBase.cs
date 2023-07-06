using SimpleStore.Entities;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Interfaces
{
    public interface ICommandRepositoryBase<T, ID> where T : EntityBase
    {
        Task<ID> AddAsync(T t);

        Task UpdateAsync();

        Task DeleteAsync(ID id);
    }
}
