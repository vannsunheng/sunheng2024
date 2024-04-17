
using APIBackend.DTOS;
using AutoMapper;
using Core.Entities;

namespace APIBackend.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        public IConfiguration Configuration { get; set; }
        public ProductUrlResolver(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return Configuration["ApiUrl"]+ source.PictureUrl;
            }
            return null;
        }
    }
}