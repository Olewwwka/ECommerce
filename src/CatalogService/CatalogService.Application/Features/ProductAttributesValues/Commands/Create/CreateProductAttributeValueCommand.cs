using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Create
{
    public record CreateProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId, string Value) : IRequest<string> { }
}
