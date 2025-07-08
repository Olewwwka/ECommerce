using CatalogService.Application.DTO.Product;
using MediatR;

namespace CatalogService.Application.UseCases.Queries.Products
{
    public record GetProductByIdQuery (Guid Id) : IRequest<ProductDto> { }
}
