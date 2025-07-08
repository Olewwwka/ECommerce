using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.UseCases.Commands.Brands
{
    public record UpdateBrandCommand(Guid Id, string Name, IFormFile? Logo) : IRequest<Guid> { }
}
