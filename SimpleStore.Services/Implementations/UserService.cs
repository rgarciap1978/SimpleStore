using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Implementations
{
    public class UserService : IUserService
    {
        public UserService(
            ) { 
        }

        public Task<ResponseDTOLogin> LoginAsync(RequestDTOLogin request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseGeneric<string>> RegisterAsync(RequestDTORegister request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> RequestTokenResetPaswordAsync(RequestDTOGenerateToken request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> ResetPasswordAsync(RequestDTOResetPassword request)
        {
            throw new NotImplementedException();
        }
    }
}
