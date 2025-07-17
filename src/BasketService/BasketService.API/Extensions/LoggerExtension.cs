using BasketService.API.Filters;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace BasketService.API.Extensions
{
    public static class LoggerExtension
    {
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.Logger(options => options
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                        new Uri(builder.Configuration.GetValue<string>("Elk")))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"ecommerce-{DateTime.UtcNow:yyyy-MM}"
                    }))
                .WriteTo.Logger(options => options
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal || e.Level == LogEventLevel.Warning)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                        new Uri(builder.Configuration.GetValue<string>("Elk")))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"ecommerce-critical-{DateTime.UtcNow:yyyy-MM}"
                    }))
                .CreateLogger();

            builder.Services.AddScoped<LogResultFilter>();
        }

    }
}
