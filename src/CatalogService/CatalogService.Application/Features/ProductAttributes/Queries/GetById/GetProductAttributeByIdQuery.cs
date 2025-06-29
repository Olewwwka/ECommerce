using CatalogService.Application.DTO.ProductAttributes;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributes.Queries.GetById
{
    public record GetProductAttributeByIdQuery(Guid CategoryId, Guid Id) : IRequest<ProductAttributeDto>
    {
    }
}
