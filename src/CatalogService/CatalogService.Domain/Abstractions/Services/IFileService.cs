using CatalogService.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Domain.Abstractions.Services
{
    public interface IFileService
    {
        Task<string> SavePhoto(IFormFile photo, FileCategory category, string? productName, string? brandName);
        Task DeletePhoto(FileCategory category, string? productName, string? brandName);
    }
}
