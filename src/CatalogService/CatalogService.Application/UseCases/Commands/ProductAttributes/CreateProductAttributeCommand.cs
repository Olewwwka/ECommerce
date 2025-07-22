using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.UseCases.Commands.ProductAttributes
{
    public record CreateProductAttributeCommand(Guid CategoryId, string Name, AttributeDataType DataType) : IRequest<string> { }
}
