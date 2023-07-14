using SimpleStore.Entities;
using SimpleStore.Entities.Infos;

namespace SimpleStore.Repository.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<(ICollection<ProductInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows);
    }
}
