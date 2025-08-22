namespace CatalogService.Domain.Entities
{
    public class ProductPriceHistory
    {
        public Guid Id { get; set; }
        public Guid ProductId {  get; set; }

        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}
