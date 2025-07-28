using Dapper;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Persistence;
using System.Data;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderReposotory
    {
        private readonly IDbConnection _connection;

        public OrderRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Guid> CreateAsync(Order order)
        {
            await _connection.ExecuteAsync(Queries.CreateOrder, order);

            return order.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _connection.ExecuteAsync(Queries.DeleteOrder, new { Id = id});

            return id;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<Order>(
                Queries.GetOrderById, new { Id = id });

            return result;
        }

        public async Task<List<Order>> GetByUserId(Guid userId)
        {
            var result = await _connection.QueryAsync<Order>(
               Queries.GetOrdersByUserId, new { UserId = userId });

            return result.ToList();
        }

        public async Task<Guid> UpdateStatusAsync(Guid orderId, OrderStatus status)
        {
            await _connection.ExecuteAsync(
                Queries.UpdateOrderStatus, new { OrderId = orderId, Status = status.ToString() });

            return orderId;
        }
    }
}
