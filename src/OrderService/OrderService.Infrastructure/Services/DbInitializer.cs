using Dapper;
using Npgsql;
using OrderService.Domain.Options;
using Microsoft.Extensions.Options;
using OrderService.Infrastructure.Persistence;
using OrderService.Domain.Abstractions.Services;

namespace OrderService.Infrastructure.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly string _connectionString;
        public DbInitializer(IOptions<DbOptions> options)
        {
            _connectionString = options.Value.OrderService;
        }

        public async Task InitializeDbAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync(CreateDbQueries.DbTables);
        }
    }
}
