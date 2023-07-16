using AutoMapper;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Client.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<ResponseDTOProduct, RequestDTOProduct>();
        }
    }
}
