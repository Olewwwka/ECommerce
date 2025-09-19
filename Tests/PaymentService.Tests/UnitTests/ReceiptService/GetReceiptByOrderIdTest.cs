using AutoMapper;
using Moq;
using PaymentService.Application.DTO;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;

namespace PaymentService.Tests.UnitTests.ReceiptServiceTests
{
    [Collection("ReceiptServiceTests")]
    public class GetReceiptByOrderIdTest
    {
        [Fact]
        public async Task ShouldReturnReceiptDTO_WhenReceiptExists()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IReceiptRepository>();

            var service = new ReceiptService(mapper.Object, repository.Object);

            var orderId = Guid.NewGuid();
            var receipt = new Receipt { OrderId = orderId };
            var expectedDto = new ReceiptDTO { OrderId = orderId };

            repository.Setup(r => r.GetByOrderIdAsync(orderId, CancellationToken.None)).ReturnsAsync(receipt);

            mapper.Setup(m => m.Map<ReceiptDTO>(receipt)).Returns(expectedDto);

            // Act
            var result = await service.GetByOrderId(orderId, CancellationToken.None);

            // Assert
            Assert.Equal(orderId, result.OrderId);
            repository.Verify(r => r.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
            mapper.Verify(m => m.Map<ReceiptDTO>(receipt), Times.Once);
        }
    }
}
