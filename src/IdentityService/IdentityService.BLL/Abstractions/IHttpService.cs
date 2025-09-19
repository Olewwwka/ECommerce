using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service responsible for HTTP requests to other services.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Recieve receipt for a given order.
        /// </summary>
        /// <param name="orderId">Unique ID of order</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Receipt</returns>
        Task<Receipt> TryGetReceiptAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
