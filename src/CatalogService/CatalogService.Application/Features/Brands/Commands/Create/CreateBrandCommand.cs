using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Features.Brands.Commands.Create
{
    public record CreateBrandCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; }
    }
}
