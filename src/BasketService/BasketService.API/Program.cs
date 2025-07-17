using BasketService.API.Extensions;
using BasketService.API.Filters;
using BasketService.Application.Mappers;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Application.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

builder.WebHost.UseUrls("http://localhost:7000");

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
        Title = "Basket Service API",
        Version = "v1",
    });
});

services.AddValidatorsFromAssembly(typeof(DeleteBasketCommandValidator).Assembly);

services.AddAutoMapper(typeof(BasketProfile).Assembly);

builder.ConnectToDatabase();

services.AddRepositories();

builder.ConfigureOptions();

services.AddMediatR(m =>
    m.RegisterServicesFromAssembly(typeof(DeleteBasketCommand).Assembly));


var app = builder.Build();


app.AddMiddlewares();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API");
    options.RoutePrefix = "";
});

app.Run();

