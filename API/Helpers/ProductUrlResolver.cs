using API.DTOs;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
namespace API.Helpers
{
    public class ProductUrlResolver:IValueResolver<Product,ProductToReturnDto,string>
    {
        public readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config=config;

        }
        public string Resolve(Product source, ProductToReturnDto dest, string destMember,ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                string httpPictureUrl= _config["ApiUrl"]+source.PictureUrl;
                return httpPictureUrl;
            }
            return null;
        }
        
    }
}