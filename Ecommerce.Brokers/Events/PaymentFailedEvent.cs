namespace Ecommerce.Brokers.Events
{
    public record PaymentFailedEvent
    {
        public Guid UserId { get; init; }
        public Guid OrderId { get; init; }
        public string Message { get; init; }
        public PaymentFailedEvent(Guid userId, Guid orderId, string message)
        {
            UserId = userId;
            OrderId = orderId;
            Message = message;
        }
    }
}
