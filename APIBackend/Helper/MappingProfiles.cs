
using System.Security.Cryptography.Xml;
using APIBackend.DTOS;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace APIBackend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
            .ForMember(xx => xx.productBrand, xxx => xxx.MapFrom(x => x.productBrand.Name))
            .ForMember(xx => xx.ProductType, xxx => xxx.MapFrom(x => x.ProductType.Name))
            .ForMember(xx => xx.PictureUrl, x => x.MapFrom<ProductUrlResolver>());
            CreateMap<Address,AddressDTO>().ReverseMap();
            CreateMap<BasketItemDTO,BasketItem>();
            CreateMap<CustomerBasketDTO,CustomerBasket>();

        }
    }
}