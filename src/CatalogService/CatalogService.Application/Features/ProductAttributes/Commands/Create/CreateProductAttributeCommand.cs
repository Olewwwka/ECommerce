using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributes.Comands.Create
{
    public record CreateProductAttributeCommand(Guid CategoryId, string Name, AttributeDataType DataType) : IRequest<string> { }
}
