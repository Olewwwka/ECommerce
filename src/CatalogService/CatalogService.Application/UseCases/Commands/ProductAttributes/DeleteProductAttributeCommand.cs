using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributes
{
    public record DeleteProductAttributeCommand (Guid CategoryId, Guid Id) : IRequest<Guid> { }
}
