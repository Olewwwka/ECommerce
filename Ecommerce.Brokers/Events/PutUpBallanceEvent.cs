namespace Ecommerce.Brokers.Events
{
    public class PutUpBallanceEvent
    {
        public Guid UserId { get; set; }
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public PutUpBallanceEvent(Guid userId, decimal amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
