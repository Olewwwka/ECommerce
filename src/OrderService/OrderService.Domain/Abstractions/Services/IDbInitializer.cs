namespace OrderService.Domain.Abstractions.Services
{
    public interface IDbInitializer
    {
        Task InitializeDbAsync();
    }
}
