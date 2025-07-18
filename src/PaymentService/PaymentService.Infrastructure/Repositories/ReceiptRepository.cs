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
    }
}
