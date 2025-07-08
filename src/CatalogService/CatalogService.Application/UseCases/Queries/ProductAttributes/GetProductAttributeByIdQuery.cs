using CatalogService.Application.DTO.ProductAttributes;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.ProductAttributes
{
    public record GetProductAttributeByIdQuery(Guid CategoryId, Guid Id) : IRequest<ProductAttributeDto>
    {
    }
}
