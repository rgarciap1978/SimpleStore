using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Entities.Infos;
using SimpleStore.Repository.Interfaces;

namespace SimpleStore.Repository.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        protected ProductRepository(SimpleStoreDBContext context) 
            : base(context)
        {
        }

        public async Task<(ICollection<ProductInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows)
        {
            return await base.ListAsync(x => !x.IsDeleted && x.Name.Contains(filter ?? string.Empty),
                predicado => new ProductInfo()
                {
                    Id = predicado.Id,
                    SkuCode = predicado.SkuCode,
                    Name = predicado.Name,
                    UnitPrice = predicado.UnitPrice,
                    Image = predicado.Image,
                    Comment = predicado.Comment,
                    Status = predicado.Status,
                    CategoryId = predicado.Category.Id,
                    CategoryName = predicado.Category.Name
                },
                orden => orden.Name,
                page,
                rows
                );
        }

     
    }
}
