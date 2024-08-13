﻿using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

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

            CreateMap<CustomerBasketDro, CustomerBasket>();

            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.DelivryMethod , o => o.MapFrom(s => s.DelivryMethod.ShortName))
                .ForMember(o => o.DelivryMethodCost , o => o.MapFrom(s => s.DelivryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(o => o.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(o => o.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl));
        }
    }
}
