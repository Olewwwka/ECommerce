using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Update
{
    public record UpdateProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId, string Value) : IRequest<Guid>
    {
    }
}
