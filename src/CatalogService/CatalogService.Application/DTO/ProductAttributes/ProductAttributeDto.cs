using CatalogService.Domain.Enums;

namespace CatalogService.Application.DTO.ProductAttributes
{
    public class ProductAttributeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AttributeDataType DataType { get; set; }
    }
}
