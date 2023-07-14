using AutoMapper;
using SimpleStore.Entities;
using SimpleStore.Entities.Infos;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductInfo, ResponseDTOProduct>();

            CreateMap<RequestDTOProduct, Product>()
                .ForMember(d => d.SkuCode, o => o.MapFrom(m => m.SkuCode))
                .ForMember(d => d.Name, o => o.MapFrom(m => m.Name))
                .ForMember(d => d.Image, o => o.MapFrom(m => m.Image))
                .ForMember(d => d.Comment, o => o.MapFrom(m => m.Comment))
                .ForMember(d => d.Status, o => o.MapFrom(d => d.Status))
                .ForMember(d => d.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(d => d.Category, o => o.Ignore());

            CreateMap<Product, ResponseDTOProduct>()
                .ForMember(d => d.Id, o => o.MapFrom(m => m.Id))
                .ForMember(d => d.SkuCode, o => o.MapFrom(m => m.SkuCode))
                .ForMember(d => d.Name, o => o.MapFrom(m => m.Name))
                .ForMember(d => d.Image, o => o.MapFrom(m => m.Image))
                .ForMember(d => d.Comment, o => o.MapFrom(m => m.Comment))
                .ForMember(d => d.Status, o => o.MapFrom(d => d.Status))
                .ForMember(d => d.StringStatus, o => o.MapFrom(d => d.Status ? "Activo" : "Inactivo"))
                .ForMember(d => d.CategoryId, o => o.MapFrom(m => m.CategoryId))
                .ForMember(d => d.CategoryName, o => o.MapFrom(m => m.Category.Name));
        }
    }
}
