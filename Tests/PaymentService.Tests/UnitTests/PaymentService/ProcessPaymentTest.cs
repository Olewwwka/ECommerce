using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Moq;
using PaymentService.Application.DTO;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Tests.UnitTests.PaymentService
{
    [Collection("PaymentServiceTests")]
    public class ProcessPaymentTest
    {
        [Fact]
        public async Task ShouldCreateReceipt_WhenPaymentSuccess()
        {
            // Arrange
            var repository = new Mock<IReceiptRepository>();
            var mapperMock = new Mock<IMapper>();
            var producer = Mock.Of<IEventProducer>();

            var service = new PayService(repository.Object, mapperMock.Object, producer);

            var request = new PaymentRequest
            {
                OrderId = Guid.NewGuid(),
                TotalCost = 100
            };

            var receipt = new Receipt { Id = Guid.NewGuid() };

            mapperMock.Setup(m => m.Map<Receipt>(request)).Returns(receipt);

            //Act
            var result = await service.ProcessPaymentAsync(request, CancellationToken.None);

            //Assert
            Assert.Equal(PaymentStatus.Success, result.Status);
            Assert.Equal(receipt.Id, result.ReceiptId);
            repository.Verify(r => r.AddAsync(receipt, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
