namespace CatalogService.Domain.Options
{
    public class FilesOptions
    {
        public string BasePath { get; set; }
        public string BrandLogosFolder { get; set; }
        public string ProductsPhotoFolder { get; set; }
        public string DefaultLogo { get; set; }
        public string DefaultProductPhoto { get; set; }
        public string[] AllowedExtensions { get; set; }
        public long MaxFileSize { get; set; }
    }
}
