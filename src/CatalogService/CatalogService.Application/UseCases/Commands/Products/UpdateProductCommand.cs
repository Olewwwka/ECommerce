using CatalogService.Application.DTO.Product;
using MediatR;

namespace CatalogService.Application.UseCases.Commands.Products
{
    public record UpdateProductCommand(Guid Id, UpdateProductDto product) : IRequest<ProductDto> { }
}
