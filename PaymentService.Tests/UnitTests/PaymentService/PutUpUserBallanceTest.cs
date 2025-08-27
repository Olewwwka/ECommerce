using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Moq;
using PaymentService.Application.DTO;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;
using PaymentService.Application.Services;

namespace PaymentService.Tests.UnitTests.PaymentService
{
    [Collection("PaymentServiceTests")]
    public class PutUpUserBallanceTest
    {
        [Fact]
        public async Task ShouldCreateReceipt()
        {
            // Arrange
            var repository = new Mock<IReceiptRepository>();
            var mapper = new Mock<IMapper>();
            var producer = Mock.Of<IEventProducer>();

            var service = new PayService(repository.Object, mapper.Object, producer);

            var request = new PutUpUserBallanceDTO
            {
                UserId = Guid.NewGuid(),
                Amount = 50
            };

            var receipt = new Receipt();
            var receiptDto = new ReceiptDTO();

            mapper.Setup(m => m.Map<Receipt>(request)).Returns(receipt);
            mapper.Setup(m => m.Map<ReceiptDTO>(receipt)).Returns(receiptDto);

            //Act
            var result = await service.PutUpUserBallance(request, CancellationToken.None);

            //Assert
            repository.Verify(r => r.AddAsync(receipt, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(receiptDto, result);
            mapper.Verify(m => m.Map<Receipt>(request), Times.Once);
            mapper.Verify(m => m.Map<ReceiptDTO>(receipt), Times.Once);
        }
    }
}
