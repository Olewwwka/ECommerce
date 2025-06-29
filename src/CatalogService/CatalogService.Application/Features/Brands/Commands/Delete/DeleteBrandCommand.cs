using MediatR;

namespace CatalogService.Application.Features.Brands.Commands.Delete
{
    public record DeleteBrandCommand (Guid Id): IRequest<bool> { }
}
