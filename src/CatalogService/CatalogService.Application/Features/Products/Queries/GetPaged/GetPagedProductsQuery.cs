using CatalogService.Application.DTO.Product;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetPaged
{
    public class GetPagedProductsQuery : IRequest<PagedItems<ProductDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
