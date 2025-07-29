namespace CatalogService.Application.DTO.ProductAttributeValues
{
    public class ProductAttributeValueDto
    {
        public Guid ProductAttributeId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
