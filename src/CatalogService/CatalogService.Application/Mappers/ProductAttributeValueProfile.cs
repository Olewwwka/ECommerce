using AutoMapper;
using CatalogService.Application.DTO;
using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Application.Features.ProductAttributesValues.Commands.Create;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class ProductAttributeValueProfile :Profile
    {
        public ProductAttributeValueProfile()
        {
            CreateMap<CreateProductAttributeValueCommand, ProductAttributeValue>();
            CreateMap<ProductAttributeValue, ProductAttributeValueDto>();
            CreateMap<ProductAttributeValueDto, ProductAttributeValue>();
        }
    }
}
