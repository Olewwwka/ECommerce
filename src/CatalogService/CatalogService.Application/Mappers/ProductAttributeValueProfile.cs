using AutoMapper;
using CatalogService.Application.DTO;
using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Application.UseCases.Commands.ProductAttributeValues;
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
