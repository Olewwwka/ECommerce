using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<string> { }
}
