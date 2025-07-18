using MongoDB.Driver;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;

namespace PaymentService.Infrastructure.Repositories
{
    public class ReceiptRepository : RepositoryBase<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IMongoDatabase database, string collection) : base(database, collection)
        {
        }
        public async Task<Receipt> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var filter = Builders<Receipt>.Filter.Eq("OrderId", orderId);

            var result = await _collection.FindAsync(filter, null, cancellationToken);

            var receipt = await result.FirstOrDefaultAsync(cancellationToken);

            return receipt;
        }
    }
}
