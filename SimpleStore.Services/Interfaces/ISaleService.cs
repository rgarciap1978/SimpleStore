using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface ISaleService
    {
        Task<ResponseGeneric<int>> AddAsync(string email, RequestDTOSale request);
        Task<ResponseGeneric<ResponseDTOSale>> GetAsync(int id);
        Task<ResponsePagination<ResponseDTOSale>> ListAsync(string email, string? filter, int page, int rows);
    }
}
