using MediatR;

namespace CatalogService.Application.UseCases.Commands.Categories
{
    public record CreateCategoryCommand(string Name) : IRequest<string> { }
}
