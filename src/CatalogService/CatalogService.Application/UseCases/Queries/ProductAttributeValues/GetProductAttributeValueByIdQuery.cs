using CatalogService.Application.DTO.ProductAttributeValues;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.ProductAttributeValues
{
    public record GetProductAttributeValueByIdQuery(Guid ProductId, Guid ProductAttributeId) : IRequest<ProductAttributeValueDto>
    {
    }
}
