using SimpleStore.Services.Implementations;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface ICategoryService : ICommandServiceBase<RequestCategoryDTO, int>, IQueryServiceBase<ResponseCategoryDTO, int>
    {
    }
}

