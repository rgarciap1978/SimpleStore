using SimpleStore.Entities;

namespace SimpleStore.Repository.Interfaces
{
    public interface ICategoryRepository : ICommandRepositoryBase<Category, int>, IQueryRepositoryBase<Category, int>
    {
    }
}
