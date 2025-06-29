using CatalogService.Application.DTO;
using MediatR;

namespace CatalogService.Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Guid> { }
}
