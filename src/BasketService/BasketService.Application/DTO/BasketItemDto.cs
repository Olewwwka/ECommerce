namespace BasketService.Application.DTO
{
    /// <summary>
    /// DTO for item in basket.
    /// </summary>
    public class BasketItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
