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
            _connection.Open();

            using var transaction = _connection.BeginTransaction();

            try
            {
                await _connection.ExecuteAsync(Queries.CreateOrder, new
                {
                    order.Id,
                    order.UserId,
                    Status = order.Status.ToString(),
                    CreatedAt = order.CreatedAt,
                    order.TotalPrice
                }, transaction);

                foreach (var item in order.Items)
                {
                    await _connection.ExecuteAsync(Queries.AddItem, new
                    {
                        item.Id,
                        OrderId = order.Id,
                        item.ProductId,
                        item.Price,
                        item.Count
                    }, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return order.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _connection.ExecuteAsync(Queries.DeleteOrder, new
            {
                Id = id
            });

            return id;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var orderDictionary = new Dictionary<Guid, Order>();

            var result = await _connection.QueryAsync<Order, OrderItem, Order>(
                Queries.GetOrderById,
                (order, item) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.Items = new List<OrderItem>();
                        orderDictionary.Add(currentOrder.Id, currentOrder);
                    }

                    if (item != null && item.Id != Guid.Empty)
                    {
                        currentOrder.Items.Add(item);
                    }

                    return currentOrder;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return orderDictionary.Values.FirstOrDefault();
        }

        public async Task<List<Order>> GetByUserId(Guid userId)
        {
            var orderDictionary = new Dictionary<Guid, Order>();

            var result = await _connection.QueryAsync<Order, OrderItem, Order>(
                Queries.GetOrdersByUserId,
                (order, item) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.Items = new List<OrderItem>();
                        orderDictionary.Add(currentOrder.Id, currentOrder);
                    }

                    if (item != null && item.Id != Guid.Empty)
                    {
                        currentOrder.Items.Add(item);
                    }

                    return currentOrder;
                },
                new { UserId = userId },
                splitOn: "Id"
            );

            return orderDictionary.Values.ToList();
        }

        public async Task<Guid> UpdateStatusAsync(Guid orderId, OrderStatus status)
        {
            await _connection.ExecuteAsync(Queries.UpdateOrderStatus, new
            {
                OrderId = orderId,
                Status = status.ToString()
            });

            return orderId;
        }
    }
}
