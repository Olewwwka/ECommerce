namespace IdentityService.DAL.Entities
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public decimal TotalCost { get; set; }
    }
}
