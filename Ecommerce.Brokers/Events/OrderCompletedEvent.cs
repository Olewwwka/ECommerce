namespace Ecommerce.Brokers.Events
{
    public record OrderCompletedEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public DateTime CompletedAt { get; init; }
    }
}
