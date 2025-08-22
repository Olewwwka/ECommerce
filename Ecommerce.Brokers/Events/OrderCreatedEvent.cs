namespace Ecommerce.Brokers.Events
{
    public record OrderCreatedEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public OrderCreatedEvent(Guid orderId, Guid userId, decimal amount)
        {
            OrderId = orderId;
            UserId = userId;
            Amount = amount;
        }
    }
}
