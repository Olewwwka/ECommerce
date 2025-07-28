namespace OrderService.Infrastructure.Persistence
{
    public static class Queries
    {
        public static string GetOrderById = @"select * from orders where id = @Id";

        public static string GetOrdersByUserId = @"select * from orders where user_id = @UserId";

        public static string UpdateOrderStatus = @"update orders set status = @Status where id = @Id";

        public static string DeleteOrder = @"delete from orders where id = @Id";

        public static string CreateOrder = @"insert into orders (id, user_id, status, created_at, total_price)" +
            " values (@Id, @UserId, @Status, @CreatedAt, @TotalPrice)";

        public static string AddItem = @"insert into order_items(id, order_id, product_id, price, count)" +
            " values(@Id, @OrderId, @ProductId, @Price, @Count)";
    }
}
