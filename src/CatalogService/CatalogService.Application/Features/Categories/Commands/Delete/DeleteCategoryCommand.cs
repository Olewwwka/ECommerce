using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<bool> { }
}
