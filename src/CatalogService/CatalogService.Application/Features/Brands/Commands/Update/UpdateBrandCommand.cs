using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Features.Brands.Commands.Update
{
    public record UpdateBrandCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; }
    }
}
