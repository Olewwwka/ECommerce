using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributeValues
{
    public record DeleteProductAttributeValueCommand(Guid ProductId, Guid ProductAttributeId) : IRequest<bool>
    {
    }
}
