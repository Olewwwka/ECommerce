namespace BasketService.Domain.Entities
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
