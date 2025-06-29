using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Features.Brands.Commands.Update
{
    public record UpdateBrandCommand(Guid Id, string Name, IFormFile? Logo) : IRequest<Guid> { }
}
