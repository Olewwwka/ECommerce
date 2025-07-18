namespace PaymentService.Domain.Options
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; } 
        public string Database { get; set; } 
        public string ReceiptCollection { get; set; }
    }
}
