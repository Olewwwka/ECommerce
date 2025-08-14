namespace Ecommerce.Brokers.Events
{
    public record ProductDeletedEvent
    {
        public Guid ProductId { get; set; }
    }
}
