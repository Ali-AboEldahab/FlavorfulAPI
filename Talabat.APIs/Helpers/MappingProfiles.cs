using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(d => d.Brand,o => o.MapFrom(n => n.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(n => n.Category.Name))
                .ForMember(d => d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
