using FluentValidation;
using Microsoft.OpenApi.Models;
using OrderService.API.Extensions;
using OrderService.API.Extentions;
using OrderService.API.Filters;
using OrderService.Application.Mappers;
using OrderService.Application.UseCases.Commands;
using OrderService.Application.Validators;
using OrderService.Domain.Abstractions.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

builder.AddLogger();

builder.Host.UseSerilog();

services.AddControllers(options =>
{
    options.Filters.Add<LogResultFilter>();
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Service API",
        Version = "v1",
    });
});


services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

services.AddAutoMapper(typeof(OrderProfile).Assembly);

builder.ConfigureOptions();
builder.AddDb();

services.AddRepositories();
services.AddServices();

services.AddMediatR(m =>
    m.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

var app = builder.Build();

app.AddMiddlewares();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service API");
    options.RoutePrefix = "";
});

var initializer = app.Services.GetRequiredService<IDbInitializer>();
await initializer.InitializeDbAsync();

app.Run();

