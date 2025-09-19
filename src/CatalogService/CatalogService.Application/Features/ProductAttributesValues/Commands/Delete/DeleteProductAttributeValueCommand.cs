using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Delete
{
    public record DeleteProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId) : IRequest<bool>
    {
    }
}
