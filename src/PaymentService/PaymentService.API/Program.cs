using Microsoft.OpenApi.Models;
using PaymentService.API.Extensions;
using PaymentService.Domain.Abstractions.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

builder.ConfigureOptions();
builder.ConnectToDb();
builder.AddRepositories();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Payment Service API",
        Version = "v1",
    });
});

builder.ConfigureServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IMongoInitializer>();
    await initializer.InitializeAsync();
}


app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Service API");
    options.RoutePrefix = "";
});

app.Run();

