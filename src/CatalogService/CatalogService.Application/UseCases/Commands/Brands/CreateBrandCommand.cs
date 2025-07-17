using MediatR;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.UseCases.Commands.Brands
{
    public record CreateBrandCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; }
    }
}
