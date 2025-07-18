using MongoDB.Driver;
using PaymentService.Domain.Abstractions.Services;

namespace PaymentService.Infrastructure.Data
{
    public class MongoInitializer : IMongoInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoInitializer(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task InitializeAsync()
        {
            var collections = await _database.ListCollectionNames().ToListAsync();
            if (!collections.Contains("receipts"))
            {
                await _database.CreateCollectionAsync("receipts");
            }
        }
    }
}
