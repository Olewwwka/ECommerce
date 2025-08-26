using AutoMapper;
using Moq;
using PaymentService.Application.DTO;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;

namespace PaymentService.Tests.UnitTests.ReceiptServiceTests
{
    [Collection("ReceiptServiceTests")]
    public class GetReceiptByIdTest
    {
        [Fact]
        public async Task ShouldReturnReceiptDTO_WhenReceiptExists()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IReceiptRepository>();

            var service = new ReceiptService(mapper.Object, repository.Object);

            var receiptId = Guid.NewGuid();
            var receipt = new Receipt { Id = receiptId };
            var expectedDto = new ReceiptDTO { Id = receiptId };

            repository.Setup(r => r.GetByIdAsync(receiptId, CancellationToken.None)).ReturnsAsync(receipt);

            mapper.Setup(m => m.Map<ReceiptDTO>(receipt)).Returns(expectedDto);

            // Act
            var result = await service.GetByIdAsync(receiptId, CancellationToken.None);

            // Assert
            Assert.Equal(receiptId, result.Id);
            repository.Verify(r => r.GetByIdAsync(receiptId, CancellationToken.None), Times.Once);
            mapper.Verify(m => m.Map<ReceiptDTO>(receipt), Times.Once);
        }
    }
}
