using MediatR;

namespace CatalogService.Application.Features.ProductAttributes.Commands.Delete
{
    public record DeleteProductAttributeCommand (Guid CategoryId, Guid Id) : IRequest<Guid> { }
}
