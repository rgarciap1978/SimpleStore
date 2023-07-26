using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponsePagination<ResponseDTOProduct>> ListAsync(int id);

        Task<ResponsePagination<ResponseDTOProduct>> ListAsync(string? filter, int page, int rows);

        Task<ResponseGeneric<ResponseDTOProduct>> FindByIdAsync(int id);

        Task<ResponseGeneric<int>> AddAsync(RequestDTOProduct request);

        Task<ResponseBase> UpdateAsync(int id, RequestDTOProduct request);

        Task<ResponseBase> DeleteAsync(int id);
    }
}
