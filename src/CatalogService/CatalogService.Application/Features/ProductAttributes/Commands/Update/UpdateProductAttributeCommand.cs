using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributes.Commands.Update
{
    public record UpdateProductAttributeCommand(Guid CategoryId, Guid Id, string Name, AttributeDataType DataType) : IRequest<Guid> { }
}
