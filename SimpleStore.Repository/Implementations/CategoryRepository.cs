using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Entities.Infos;
using SimpleStore.Repository.Interfaces;

namespace SimpleStore.Repository.Implementations
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(SimpleStoreDBContext context) 
            : base(context)
        {
        }

        public async Task<(ICollection<CategoryInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows)
        {
            return await base.ListAsync(x => !x.IsDeleted && x.Name.Contains(filter ?? string.Empty),
                predicado => new CategoryInfo()
                {
                    Id = predicado.Id,
                    Name = predicado.Name,
                    IsDeleted = predicado.IsDeleted ? "Eliminado" : ""
                },
                orden => orden.Name,
                page,
                rows
                );
        }
    }
}
