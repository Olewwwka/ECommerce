using CatalogService.Application.DTO.Product;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery (Guid Id) : IRequest<ProductDto> { }
}
