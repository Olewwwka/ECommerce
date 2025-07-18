namespace CatalogService.Application.DTO.Product
{
    public record ProductFilterDto
    {
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public string? SearchText { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
