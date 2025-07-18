using CatalogService.Domain.Enums;

namespace CatalogService.Application.DTO.ProductAttributes
{
    public record CreateProductAttributeDto(string Name, AttributeDataType DataType) { }
}
