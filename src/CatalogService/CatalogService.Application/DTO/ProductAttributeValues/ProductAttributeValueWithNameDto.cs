namespace CatalogService.Application.DTO.ProductAttributeValues
{
    public class ProductAttributeValueWithNameDto
    {
        public Guid ProductAttributeId { get; set; }
        public string AttributeName {  get; set; }
        public string AttributeValue { get; set; }
    }
}
