using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseGeneric<ICollection<ResponseDTOCategory>>> ListAsync();

        Task<ResponsePagination<ResponseDTOCategory>> ListAsync(string? filter, int page, int rows);

        Task<ResponseGeneric<ResponseDTOCategory>> FindByIdAsync(int id);

        Task<ResponseGeneric<int>> AddAsync(RequestDTOCategory request);

        Task<ResponseBase> UpdateAsync(int id, RequestDTOCategory request);

        Task<ResponseBase> DeleteAsync(int id);
    }
}

