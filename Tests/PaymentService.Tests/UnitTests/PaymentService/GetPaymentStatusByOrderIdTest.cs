using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Moq;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Tests.UnitTests.PaymentService
{
    [Collection("PaymentServiceTests")]
    public class GetPaymentStatusByOrderIdTest
    {
        [Fact]
        public async Task ShouldReturnStatus_WhenPaymentExists()
        {
            // Arrange
            var repository = new Mock<IReceiptRepository>();
            var mapper = Mock.Of<IMapper>();
            var producer = Mock.Of<IEventProducer>();

            var service = new PayService(repository.Object, mapper, producer);

            var orderId = Guid.NewGuid();
            var expectedStatus = PaymentStatus.Success;
            var receipt = new Receipt { Status = expectedStatus };

            repository.Setup(r => r.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>())).ReturnsAsync(receipt);

            // Act
            var actualStatus = await service.GetPaymentStatusByOrderIdAsync(orderId, CancellationToken.None);

            // Assert
            Assert.Equal(expectedStatus, actualStatus);
            repository.Verify(r => r.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
