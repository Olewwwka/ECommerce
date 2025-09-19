namespace BasketService.Domain.Entities
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; }
        public void AddItem(BasketItem item)
        {
            if (Items == null)
            {
                Items = new List<BasketItem>();
            }
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(Guid productId, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem == null) return;

            existingItem.Quantity -= quantity;
            if (existingItem.Quantity <= 0)
                Items.Remove(existingItem);
        }
    }
}
