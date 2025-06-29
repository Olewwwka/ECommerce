using CatalogService.Application.DTO.Product;
using MediatR;

namespace CatalogService.Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, UpdateProductDto product) : IRequest<ProductDto> { }
}
