using SimpleStore.Entities;
using SimpleStore.Services.Implementations;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponsePagination<ResponseCategoryDTO>> ListAsync(string? filter, int page, int rows);

        Task<ResponseGeneric<ResponseCategoryDTO>> FindByIdAsync(int id);

        Task<ResponseGeneric<int>> AddAsync(RequestCategoryDTO request);

        Task<ResponseBase> UpdateAsync(int id, RequestCategoryDTO request);

        Task<ResponseBase> DeleteAsync(int id);
    }
}

