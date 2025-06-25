using CatalogService.Application.DTO;
using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<BrandDto> { }
}
