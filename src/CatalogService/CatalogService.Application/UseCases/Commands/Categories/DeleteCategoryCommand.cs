using MediatR;

namespace CatalogService.Application.UseCases.Commands.Categories
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<bool> { }
}
