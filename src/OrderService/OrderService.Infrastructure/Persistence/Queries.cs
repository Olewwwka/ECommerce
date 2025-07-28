namespace OrderService.Infrastructure.Persistence
{
    public static class Queries
    {
        public static string GetOrderById = @"select 
                o.id as Id,
                o.user_id as UserId,
                o.status as Status,
                o.created_at as CreatedAt,
                o.total_price as TotalPrice,
  
                oi.id as Id,
                oi.order_id as OrderId,
                oi.product_id as ProductId,
                oi.price as Price,
                oi.count as Count
            from orders o inner join order_items oi on 
                oi.order_id = o.id where o.id = @Id";

        public static string GetOrdersByUserId = @"select 
                o.id as Id,
                o.user_id as UserId,
                o.status as Status,
                o.created_at as CreatedAt,
                o.total_price as TotalPrice,
  
                oi.id as Id,
                oi.order_id as OrderId,
                oi.product_id as ProductId,
                oi.price as Price,
                oi.count as Count
            from orders o inner join order_items oi on 
                oi.order_id = o.id where o.user_id = @UserId";

        public static string UpdateOrderStatus = @"update orders set status = @Status where id = @orderId";

        public static string DeleteOrder = @"delete from orders where id = @Id";

        public static string CreateOrder = @"insert into orders (id, user_id, status, created_at, total_price)" +
            " values (@Id, @UserId, @Status, @CreatedAt, @TotalPrice)";

        public static string AddItem = @"insert into order_items(id, order_id, product_id, price, count)" +
            " values(@Id, @OrderId, @ProductId, @Price, @Count)";
    }
}
