using Azure;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Entities.Infos;
using SimpleStore.Repository.Interfaces;
using System;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(SimpleStoreDBContext context) 
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

        public async Task<ICollection<ProductInfo>> ListByCategoryAsync(int id)
        {
            return await base.ListAsync(x => !x.IsDeleted && x.CategoryId.Equals(id),
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
                });
        }

    }
}
