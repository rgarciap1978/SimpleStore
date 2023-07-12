using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<ResponseGeneric<ICollection<T>>> ListAsync(string? filter, int page, int rows);

        Task<ResponseGeneric<T>> FindAsync(int id);

        Task<ResponseGeneric<int>> AddAsync(T t);

        Task<ResponseBase> UpdateAsync(int ind, T t);

        Task<ResponseBase> DeleteAsync(int id);
    }
}
