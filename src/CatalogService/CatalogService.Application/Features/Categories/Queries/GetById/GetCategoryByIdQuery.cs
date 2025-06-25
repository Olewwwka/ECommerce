using CatalogService.Application.DTO;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<CategoryWithAttributesDto> { }
}
