using CatalogService.Domain.Enums;

namespace CatalogService.Application.DTO.ProductAttributes
{
    public record UpdateProductAttributeDto(string Name, AttributeDataType DataType) { }
}
