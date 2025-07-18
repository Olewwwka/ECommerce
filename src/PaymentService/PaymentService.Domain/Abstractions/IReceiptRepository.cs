using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Abstractions
{
    public interface IReceiptRepository : IRepositoryBase<Receipt>
    {
        Task<Receipt> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
