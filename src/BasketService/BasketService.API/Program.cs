
using BasketService.API.Extensions;
using BasketService.Application.Mappers;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Application.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var configurations = builder.Configuration;
var services = builder.Services;


builder.ConnectToDatabase();

services.AddRepositories();

var app = builder.Build();

services.AddControllers();

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

