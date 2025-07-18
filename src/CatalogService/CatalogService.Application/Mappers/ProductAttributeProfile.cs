using AutoMapper;
using CatalogService.Application.DTO.ProductAttributes;
using CatalogService.Application.Features.ProductAttributesValues.Commands.Create;
using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<CreateProductAttributeCommand, ProductAttribute>()
                 .ForMember(dest => dest.AttributeType, opt => opt.MapFrom(src => (int)src.DataType));
            CreateMap<ProductAttribute, ProductAttributeDto>();

            CreateMap<UpdateProductAttributeCommand, ProductAttribute>();

            CreateMap(typeof(PagedItems<>), typeof(PagedItems<>))
                .ConvertUsing(typeof(PagedItemsProfile<,>));
        }
    }
}
