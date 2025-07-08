using CatalogService.Application.DTO.Product;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Products
{
    public class GetPagedProductsQuery : IRequest<PagedItems<ProductDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
