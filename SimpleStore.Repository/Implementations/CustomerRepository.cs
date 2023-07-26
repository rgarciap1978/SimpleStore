using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess;
using SimpleStore.Entities;
using SimpleStore.Repository.Interfaces;
using SimpleStore.Shared.Response;
using System.Reflection.Metadata.Ecma335;

namespace SimpleStore.Repository.Implementations
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(SimpleStoreDBContext context) 
            : base(context) 
        {
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Set<Customer>()
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
