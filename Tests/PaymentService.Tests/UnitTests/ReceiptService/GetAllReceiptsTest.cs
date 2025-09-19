using AutoMapper;
using Moq;
using PaymentService.Application.DTO;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;

namespace PaymentService.Tests.UnitTests.ReceiptServiceTests
{
    [Collection("ReceiptServiceTests")]
    public class GetAllReceiptsTest
    {
        [Fact]
        public async Task ShouldReturnListOfReceiptDTOs()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IReceiptRepository>();

            var service = new ReceiptService(mapper.Object, repository.Object);

            var receipts = new List<Receipt>
            {
                new Receipt { Id = Guid.NewGuid() },
                new Receipt { Id = Guid.NewGuid() }
            };

            var expectedDtos = receipts.Select(r => new ReceiptDTO { Id = r.Id }).ToList();

            repository.Setup(r => r.GetPaginated(1, 10, CancellationToken.None)).ReturnsAsync(receipts);

            mapper.Setup(m => m.Map<List<ReceiptDTO>>(receipts)).Returns(expectedDtos);

            // Act
            var result = await service.GetAll(1, 10, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedDtos[0].Id, result[0].Id);
            Assert.Equal(expectedDtos[1].Id, result[1].Id);

            repository.Verify(r => r.GetPaginated(1, 10, CancellationToken.None), Times.Once);
            mapper.Verify(m => m.Map<List<ReceiptDTO>>(receipts), Times.Once);
        }
    }
}
