using CatalogService.Application.DTO.ProductAttributes;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.ProductAttributes
{
    public record GetPagedProductAttributesQuery(Guid CategoryId, int PageNumber, int PageSize) : IRequest<PagedItems<ProductAttributeDto>> { }
}
