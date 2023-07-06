using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using System.Linq.Expressions;

namespace SimpleStore.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SimpleStoreDBContext _context;

        public CategoryRepository(SimpleStoreDBContext context) {
            _context = context;
        }

        public Task<int> AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(Expression<Func<Category, bool>> predicate, Expression<Func<Category, TInfo>> selector, Expression<Func<Category, TKey>> orderBy, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
