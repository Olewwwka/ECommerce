namespace Ecommerce.Brokers.Events
{
    public record ProductAddedToBasketEvent
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public ProductAddedToBasketEvent(Guid userId, Guid productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
