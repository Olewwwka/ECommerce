using CatalogService.Application.DTO.Brands;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetAll
{
    public record GetAllBrandsQuery : IRequest<PagedItems<BrandDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
