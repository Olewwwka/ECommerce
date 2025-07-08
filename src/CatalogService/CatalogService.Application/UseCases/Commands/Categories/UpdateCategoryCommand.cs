using CatalogService.Application.DTO;
using MediatR;

namespace CatalogService.Application.UseCases.Commands.Categories
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Guid> { }
}
