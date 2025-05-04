using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Modules;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Product;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile //form auto mapper
    {
        public ProductProfile() { 
                    //Source     Destination
            CreateMap<Product,ProductResponse>().
                ForMember(dest=>dest.BrandName,
                options=>options.MapFrom(src=>src.productBrand.Name))
                .ForMember(dest=>dest.TypeName,
                options=>options.MapFrom(src=>src.productType.Name))
                .ForMember(dest=>dest.PictureUrl,
                options=>options.MapFrom<PictureUrlResolver>()) ;


            CreateMap<ProductBrand, BrandResponse>();

            CreateMap<ProductType, TypeResponse>();
             
        }


    }

    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
