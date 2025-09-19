using CatalogService.Domain.Abstractions.Services;
using CatalogService.Domain.Enums;
using CatalogService.Domain.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CatalogService.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly FilesOptions _options;
        public FileService(IOptions<FilesOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> SavePhoto(IFormFile photo, FileCategory category, string? productName, string? brandName)
        {
            if (photo is null)
            {
                return category switch
                {
                    FileCategory.ProductPhoto => _options.DefaultProductPhoto,
                    FileCategory.BrandLogo => _options.DefaultLogo,
                    _ => throw new Exception()
                };
            }

            if (productName is null && brandName is null)
            {
                throw new ArgumentNullException("Name is required!");
            }

            var extension = Path.GetExtension(photo.FileName).ToLower();

            if (!_options.AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Invalid extension: {extension}");
            }

            if (photo.Length > _options.MaxFileSize)
            {
                throw new ArgumentException($"File size too long");
            }

            var relativeFolder = category switch
            {
                FileCategory.BrandLogo => _options.BrandLogosFolder,
                FileCategory.ProductPhoto => Path.Combine(_options.ProductsPhotoFolder, productName),
                _ => throw new InvalidOperationException("Invalid category of file")
            };

            var absoluteFolder = Path.Combine(_options.BasePath, relativeFolder);

            if (!Directory.Exists(absoluteFolder))
            {
                Directory.CreateDirectory(absoluteFolder);
            }

            var timestamp = DateTime.UtcNow.ToString("yyyyMMdd");

            var fileName = (productName ?? brandName) + timestamp + $"{extension}";

            var filePath = Path.Combine(absoluteFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await photo.CopyToAsync(stream);

            return Path.Combine(relativeFolder, fileName).Replace("\\", "/");

        }

        public async Task DeletePhoto(FileCategory category, string? productName, string? brandName)
        {
            switch (category)
            {
                case FileCategory.ProductPhoto:
                    if (string.IsNullOrWhiteSpace(productName))
                        throw new ArgumentException("Product name is required!!");

                    var productFolder = Path.Combine(_options.BasePath, _options.ProductsPhotoFolder, productName);

                    if (Directory.Exists(productFolder))
                    {
                        Directory.Delete(productFolder, recursive: true);
                    }
                    break;

                case FileCategory.BrandLogo:
                    if (string.IsNullOrWhiteSpace(brandName))
                        throw new ArgumentException("Brand name name is required!!");

                    var brandFolder = Path.Combine(_options.BasePath, _options.BrandLogosFolder);

                    if (!Directory.Exists(brandFolder))
                        return;

                    var files = Directory.GetFiles(brandFolder);

                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file);
                        if (fileName.StartsWith(brandName, StringComparison.OrdinalIgnoreCase))
                        {
                            File.Delete(file);
                        }
                    }
                    break;

                default:
                    throw new InvalidOperationException("Invalid category of file");
            }
        }
    }
}
