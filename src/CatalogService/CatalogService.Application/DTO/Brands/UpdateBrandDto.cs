using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.DTO.Brands
{
    public record UpdateBrandDto(string Name, IFormFile? Logo) { }
}
