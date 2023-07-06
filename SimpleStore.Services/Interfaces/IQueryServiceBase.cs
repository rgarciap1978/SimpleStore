using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface IQueryServiceBase<T, ID>
    {
        Task<ResponseGeneric<ICollection<T>>> ListAsync(string? filter, int page, int rows);

        Task<ResponseGeneric<T>> GetAsync(ID id);
    }
}
