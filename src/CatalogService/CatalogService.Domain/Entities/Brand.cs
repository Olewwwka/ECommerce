namespace CatalogService.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl {  get; set; }
        public Brand()
        {
            
        }

    }
}
