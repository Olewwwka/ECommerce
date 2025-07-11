namespace OrderService.Infrastructure.Persistence
{
    public static class CreateDbQueries
    {
		public static string DbTables = @"
			create table if not exists orders(
				id uuid primary key, 
				user_id uuid not null,
				status varchar(50) not null,
				created_at timestamp not null,
				total_price numeric(20,5) not null
				);

			create table if not exists order_items(
				id uuid primary key,
				order_id uuid not null references orders(id) on delete cascade,
				product_id uuid not null,
				price numeric(20,5) not null,
				count int not null
				);

			create index if not exists idx_orders_userid on orders (user_id);
			create index if not exists idx_orderitems_orderid on order_items (order_id);";
    }
}
