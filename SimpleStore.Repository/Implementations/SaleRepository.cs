using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Implementations
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(SimpleStoreDBContext context)
            : base(context)
        {
        }

        public async Task CreateTransaction() => await _context.Database.BeginTransactionAsync();

        public async Task RollbackTransaction() => await _context.Database.RollbackTransactionAsync();

        public override async Task<int> AddAsync(Sale entity)
        {
            entity.SaleDate= DateTime.Now;
            var lastNumber = await _context.Set<Sale>().CountAsync() + 1;
            entity.Correlative = $"{lastNumber:00000}";
            await _context.AddAsync(entity);

            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await _context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<Sale?> FindAsync(int id)
        {
            return await _context.Set<Sale>()
                .Include(p => p.Product)
                .ThenInclude(c => c.Category)
                .Include(c => c.Customer)
                .Where(s => s.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
            Expression<Func<Sale, bool>> predicate,
            Expression<Func<Sale, TInfo>> selector,
            Expression<Func<Sale, TKey>> orderBy,
            int page,
            int rows
        )
        {
            var collection = await _context.Set<Sale>()
                .Include(p => p.Product)
                .ThenInclude(c => c.Category)
                .Include(c => c.Customer)
                .Where(predicate)
                .OrderBy(orderBy)
                .Skip((page - 1) * rows)
                .Take(rows)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();

            var total = await _context.Set<Sale>()
                .Where(predicate)
                .CountAsync();

            return (collection, total);
        }
    }
}
