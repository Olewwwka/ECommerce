using CatalogService.Application.DTO.Categories;
using MediatR;

namespace CatalogService.Application.Features.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<CategoryWithAttributesDto> { }
}
