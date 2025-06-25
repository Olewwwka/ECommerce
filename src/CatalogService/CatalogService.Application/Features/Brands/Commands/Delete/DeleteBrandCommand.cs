using MediatR;

namespace CatalogService.Application.Features.Brands.Commands.Delete
{
    public record DeleteBrandCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
