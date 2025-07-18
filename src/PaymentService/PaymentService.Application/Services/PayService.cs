using AutoMapper;
using PaymentService.Application.Abstrations;
using PaymentService.Application.DTO;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Application.Services
{
    public class PayService : IPayService
    {
        private readonly IMapper _mapper;
        private readonly IReceiptRepository _repository;
        public PayService(IReceiptRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken)
        {
            var receipt = _mapper.Map<Receipt>(request);
            receipt.Status = PaymentStatus.Success;

            await _repository.AddAsync(receipt, cancellationToken);

            return new PaymentResponse
            {
                Status = PaymentStatus.Success,
                ReceiptId = receipt.Id
            };
        }

        public async Task<PaymentStatus> GetPaymentStatusByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var receipt = await _repository.GetByOrderIdAsync(orderId, cancellationToken);

            if(receipt is null)
            {
                throw new NotFoundException($"Order with id {orderId} not found =(");
            }

            var status = receipt.Status;

            return status;
        }
    }
}
