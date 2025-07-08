using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributeValues
{
    public record CreateProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId, string Value) : IRequest<string> { }
}
