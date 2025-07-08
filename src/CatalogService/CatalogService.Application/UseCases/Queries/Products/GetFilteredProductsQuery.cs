using CatalogService.Application.DTO;
using CatalogService.Application.DTO.Product;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Products
{
    public record GetFilteredProductsQuery(GetPagedItemsDto options, ProductFilterDto filter) : IRequest<PagedItems<ProductDto>> { }
}
