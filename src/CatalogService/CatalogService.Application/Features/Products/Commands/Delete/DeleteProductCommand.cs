using MediatR;

namespace CatalogService.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<Guid> { }
}
