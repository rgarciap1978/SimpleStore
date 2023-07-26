using AutoMapper;
using SimpleStore.Entities;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile() {
            
            CreateMap<RequestDTOSale, Sale>()
                .ForMember(d => d.ProductId, o => o.MapFrom(m => m.ProductId))
                .ForMember(d => d.Quantity, o => o.MapFrom(m => m.Quantity));

            CreateMap<Sale, ResponseDTOSale>()
                .ForMember(d => d.Id, o => o.MapFrom(m => m.Id))
                .ForMember(d => d.DateSale, o => o.MapFrom(m => m.DateSale))
                .ForMember(d => d.VoucherNumber, o => o.MapFrom(m => m.Correlative))
                .ForMember(d => d.Quantity, o => o.MapFrom(m => m.Quantity))
                .ForMember(d => d.Total, o => o.MapFrom(m => m.Total))
                .ForMember(d => d.CategoryName, o => o.MapFrom(m => m.Product.Category.Name))
                .ForMember(d => d.ProductName, o => o.MapFrom(m => m.Product.Name))
                .ForMember(d => d.CustomerName, o => o.MapFrom(m => m.Customer.FullName));
                
        }
    }
}
