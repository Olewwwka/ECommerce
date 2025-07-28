namespace BasketService.Application.DTO
{
    public class BasketDto
    {
        public Guid UserId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
