using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDTOLogin> LoginAsync(RequestDTOLogin request);
        Task<ResponseGeneric<string>> RegisterAsync(RequestDTORegister request);
        Task<ResponseBase> RequestTokenResetPaswordAsync(RequestDTOGenerateToken request);
        Task<ResponseBase> ResetPasswordAsync(RequestDTOResetPassword request);
    }
}
