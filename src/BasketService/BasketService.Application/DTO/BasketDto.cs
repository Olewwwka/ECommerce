namespace BasketService.Application.DTO
{
    /// <summary>
    /// DTO for user basket.
    /// </summary>
    public class BasketDto
    {
        public Guid UserId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
