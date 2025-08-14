namespace CatalogService.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProductAttribute> Attributes { get; set; } = new();
        public Category()
        {
            
        }
    }
}
