﻿using AutoMapper;
using SimpleStore.Entities;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;

namespace SimpleStore.Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {

            CreateMap<ResponseCategoryDTO, Category>()
                .ForMember(d => d.Id, o => o.MapFrom(d => d.Id))
                .ForMember(d => d.Name, o => o.MapFrom(d => d.Name))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(d => d.Status));

            CreateMap<Category, RequestCategoryDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(d => d.Name))
                .ForMember(d => d.Status, o => o.MapFrom(d => d.IsDeleted));
        }
    }
}