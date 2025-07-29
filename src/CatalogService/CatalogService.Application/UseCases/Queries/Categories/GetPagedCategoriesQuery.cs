using CatalogService.Application.DTO.Categories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Categories
{
    public record GetPagedCategoriesQuery : IRequest<PagedItems<CategoryWithAttributesDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
