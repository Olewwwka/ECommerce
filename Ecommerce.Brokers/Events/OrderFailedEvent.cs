namespace Ecommerce.Brokers.Events
{
    public record OrderFailedEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public string Reason { get; init; }
        public DateTime FailedAt { get; init; }
        public OrderFailedEvent(Guid orderId, Guid userId, string reason, DateTime failedAt)
        {
            OrderId = orderId;
            UserId = userId;
            Reason = reason;
            FailedAt = failedAt;
        }
    }
}
