using BasketService.API.Extensions;
using BasketService.API.Filters;
using BasketService.API.Handlers;
using BasketService.Application.Mappers;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Application.Validators;
using Ecommerce.Brokers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;


builder.WebHost.UseUrls("http://localhost:5003");

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

services.AddBrokers(configuration);

services.AddServices();

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Custom";
    options.DefaultChallengeScheme = "Custom";
    options.DefaultForbidScheme = "Custom";
})
.AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Custom", null);

services.AddAuthorization();

var app = builder.Build();

app.AddMiddlewares();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API");
    options.RoutePrefix = "swagger";
});

app.Run();

public partial class Program { }

