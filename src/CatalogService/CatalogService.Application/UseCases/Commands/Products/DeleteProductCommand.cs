using MediatR;

namespace CatalogService.Application.UseCases.Commands.Products
{
    public record DeleteProductCommand(Guid Id) : IRequest<Guid> { }
}
