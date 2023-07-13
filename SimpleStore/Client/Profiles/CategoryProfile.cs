using AutoMapper;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Client.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<ResponseCategoryDTO, RequestCategoryDTO>();
        }
    }
}
