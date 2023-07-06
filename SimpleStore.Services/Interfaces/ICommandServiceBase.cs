using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface ICommandServiceBase<T, ID>
    {
        Task<ResponseGeneric<ID>> AddAsync(T t);

        Task<ResponseBase> UpdateAsync(ID ind, T t);

        Task<ResponseBase> DeleteAsync(ID id);
    }
}
