using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
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
        private readonly IEventProducer _producer;
        private readonly IMapper _mapper;
        private readonly IReceiptRepository _repository;
        public PayService(IReceiptRepository repository, IMapper mapper, IEventProducer producer)
        {
            _repository = repository;
            _mapper = mapper;
            _producer = producer;
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

        public async Task<ReceiptDTO> PutUpUserBallance(PutUpUserBallanceDTO request, CancellationToken cancellationToken)
        {
            var ballanceEvent = new PutUpBallanceEvent(request.UserId, request.Amount);

            await _producer.SendAsync(ballanceEvent, cancellationToken);

            var receipt = _mapper.Map<Receipt>(request);

            await _repository.AddAsync(receipt, cancellationToken);

            var receiptDto = _mapper.Map<ReceiptDTO>(receipt);

            return receiptDto;
        }
    }
}
