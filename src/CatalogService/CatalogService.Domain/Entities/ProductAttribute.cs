using CatalogService.Domain.Enums;

namespace CatalogService.Domain.Entities
{
    public class ProductAttribute
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public AttributeDataType AttributeType { get; set; }
    }
}
