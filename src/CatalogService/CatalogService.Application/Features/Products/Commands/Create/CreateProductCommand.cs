using CatalogService.Application.DTO.ProductAttributeValues;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsAvaliable { get; set; }
        public List<ProductAttributeValueDto> AttributeValues { get; set; } = new();
    }
}
