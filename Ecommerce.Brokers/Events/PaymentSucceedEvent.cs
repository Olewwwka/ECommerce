namespace Ecommerce.Brokers.Events
{
    public record PaymentSucceedEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public DateTime PaidAt { get; init; }
        public PaymentSucceedEvent(Guid orderId, Guid userId, decimal amount, DateTime paidAt)
        {
            OrderId = orderId;
            UserId = userId;
            Amount = amount;
            PaidAt = paidAt;
        }
    }
}
