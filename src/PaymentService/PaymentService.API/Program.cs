using Microsoft.OpenApi.Models;
using PaymentService.API.Extensions;
using PaymentService.Application.Mappers;
using PaymentService.Domain.Abstractions.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Serilog;
using PaymentService.API.Filters;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddLogger();

builder.Host.UseSerilog();

services.AddControllers(options =>
{
    options.Filters.Add<LogResultFilter>();
});

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

builder.Services.AddAutoMapper(typeof(ReceiptProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IMongoInitializer>();
    await initializer.InitializeAsync();
}

app.AddMiddlewares();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Service API");
    options.RoutePrefix = "";
});

app.Run();

