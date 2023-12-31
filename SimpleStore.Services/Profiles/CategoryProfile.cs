﻿using AutoMapper;
using SimpleStore.Entities;
using SimpleStore.Entities.Infos;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryInfo, ResponseDTOCategory>();

            CreateMap<RequestDTOCategory, Category>()
                .ForMember(d => d.Name, o => o.MapFrom(d => d.Name))
                .ForMember(d => d.Status, o => o.MapFrom(d => d.Status));

            CreateMap<Category, ResponseDTOCategory>()
                .ForMember(d => d.Id, o => o.MapFrom(d => d.Id))
                .ForMember(d => d.Name, o => o.MapFrom(d => d.Name))
                .ForMember(d => d.StringStatus, o => o.MapFrom(d => d.Status ? "Activo" : "Inactivo"));
        }
    }
}
