using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributeValues
{
    public record UpdateProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId, string Value) : IRequest<Guid>
    {
    }
}
