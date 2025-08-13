namespace CatalogService.Application.DTO
{
    public record GetPagedItemsDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
