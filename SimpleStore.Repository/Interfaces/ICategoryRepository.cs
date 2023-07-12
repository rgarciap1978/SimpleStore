using SimpleStore.Entities;
using SimpleStore.Entities.Infos;

namespace SimpleStore.Repository.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<(ICollection<CategoryInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows);
    }
}
