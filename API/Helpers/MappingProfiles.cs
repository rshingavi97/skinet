using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper; //added for Profile class
using Core.Entities;
namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(dest=>dest.ProductBrand,opt=>opt.MapFrom(sou=>sou.ProductBrand.Name))
                .ForMember(dest=>dest.ProductType,opt=>opt.MapFrom(sou=>sou.ProductType.Name))
                .ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<ProductUrlResolver>());
        }
    }
}