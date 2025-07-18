using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Abstrations;
using PaymentService.Application.DTO;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayService _paymentService;
        public PaymentController(IPayService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.ProcessPaymentAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("status/{orderId:guid}")]
        public async Task<IActionResult> GetPaymentStatusByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _paymentService.GetPaymentStatusByOrderIdAsync(orderId, cancellationToken);
            return Ok(result);
        }
    }
}
