namespace OrderService.Application.DTO
{
    public record CreateOrderDto
    {
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
