using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributes
{
    public record UpdateProductAttributeCommand(Guid CategoryId, Guid Id, string Name, AttributeDataType DataType) : IRequest<Guid> { }
}
