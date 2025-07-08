using MediatR;

namespace CatalogService.Application.UseCases.Commands.Brands
{
    public record DeleteBrandCommand (Guid Id): IRequest<bool> { }
}
