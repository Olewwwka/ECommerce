using CatalogService.Application.DTO.Categories;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Categories
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<CategoryWithAttributesDto> { }
}
