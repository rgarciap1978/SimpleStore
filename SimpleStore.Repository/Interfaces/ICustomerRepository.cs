using SimpleStore.Entities;

namespace SimpleStore.Repository.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}
