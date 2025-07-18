namespace PaymentService.Domain.Abstractions.Services
{
    public interface IMongoInitializer
    {
        Task InitializeAsync();
    }
}
