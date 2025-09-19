using CatalogService.Application.DTO.ProductAttributeValues;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Queries.GetById
{
    public record GetProductAttributeValueByIdQuery(Guid ProductId, Guid ProductAttributeId) : IRequest<ProductAttributeValueDto>
    {
    }
}
