using SimpleStore.Entities;

namespace SimpleStore.Repository.Interfaces
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransaction();
        Task RollbackTransaction();
    }
}
