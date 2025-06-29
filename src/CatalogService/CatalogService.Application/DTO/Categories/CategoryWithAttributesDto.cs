
using CatalogService.Application.DTO.ProductAttributes;

namespace CatalogService.Application.DTO.Categories
{
    public record CategoryWithAttributesDto(Guid Id, string Name, List<ProductAttributeDto> Attributes) { }
}
