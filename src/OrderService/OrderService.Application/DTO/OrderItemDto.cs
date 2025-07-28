namespace OrderService.Application.DTO
{
    public record OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice => Price * Count;
    }
}
