namespace CatalogService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId {  get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public bool IsAvaliable { get; set; }
        public List<ProductImage> Images { get; set; } = new();
        public List<ProductAttributeValue> AttributeValues { get; set; } = new();
        public Product()
        {
            
        }
    }
}
