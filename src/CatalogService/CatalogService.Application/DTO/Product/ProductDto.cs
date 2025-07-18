using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.DTO.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsAvaliable { get; set; }
        public List<ProductAttributeValueWithNameDto> AttributeValues { get; set; } = new();
    }
}
