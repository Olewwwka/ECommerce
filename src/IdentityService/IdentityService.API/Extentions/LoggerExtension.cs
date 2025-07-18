using IdentityService.Api.Filters;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Events;

namespace IdentityService.Api.Extentions
{
    public static class LoggerExtension
    {
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            var serviceName = builder.Environment.ApplicationName;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithProperty("ServiceName", serviceName)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.Logger(options => options
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                        new Uri(builder.Configuration.GetValue<string>("Elk")))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"ecommerce-identity-{DateTime.UtcNow:yyyy-MM}"
                    }))
                .WriteTo.Logger(options => options
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal || e.Level == LogEventLevel.Warning)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
                        new Uri(builder.Configuration.GetValue<string>("Elk")))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"critical-ecommerce-identity-{DateTime.UtcNow:yyyy-MM}"
                    }))
                .CreateLogger();

            builder.Services.AddScoped<LogResultFilter>();
        }

    }
}
