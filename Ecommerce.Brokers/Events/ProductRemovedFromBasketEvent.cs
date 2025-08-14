namespace Ecommerce.Brokers.Events
{
    public record ProductRemovedFromBasketEvent
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public ProductRemovedFromBasketEvent(Guid userId, Guid productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
