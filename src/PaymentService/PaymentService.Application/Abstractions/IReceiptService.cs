using PaymentService.Application.DTO;

namespace PaymentService.Application.Abstractions
{
    public interface IReceiptService
    {
        Task<ReceiptDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ReceiptDTO> GetByOrderId(Guid orderId, CancellationToken cancellationToken);
        Task<List<ReceiptDTO>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
