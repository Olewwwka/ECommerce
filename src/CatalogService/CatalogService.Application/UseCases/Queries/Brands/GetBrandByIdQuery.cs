using CatalogService.Application.DTO.Brands;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Brands
{
    public record GetBrandByIdQuery(Guid Id) : IRequest<BrandDto> { }
}
