using AutoMapper;
using CatalogService.Application.DTO.Product;
using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Application.Features.Products.Commands.Create;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>()
                 .ForMember(dest => dest.AttributeValues, opt => opt.MapFrom(src => src.AttributeValues));
            CreateMap<Product, ProductDto>();

            CreateMap(typeof(PagedItems<>), typeof(PagedItems<>))
                .ConvertUsing(typeof(PagedItemsProfile<,>));

            CreateMap<ProductFilterDto, ProductFilter>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.AttributeValues, opt => opt.MapFrom(src => src.AttributeValues));

            CreateMap<ProductAttributeValue, ProductAttributeValueWithNameDto>()
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute.Name));

            CreateMap<UpdateProductDto, Product>();
        }
    }
}
