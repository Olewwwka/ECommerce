namespace OrderService.Application.DTO
{
    public record CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
