using CatalogService.Application.DTO;
using CatalogService.Application.DTO.Product;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetFiltered
{
    public record GetFilteredProductsQuery(GetPagedItemsDto options, ProductFilterDto filter) : IRequest<PagedItems<ProductDto>> { }
}
