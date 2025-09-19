using Ecommerce.Brokers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PaymentService.API.Extensions;
using PaymentService.API.Filters;
using PaymentService.API.Handlers;
using PaymentService.Application.Mappers;
using PaymentService.Domain.Abstractions.Services;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


builder.AddLogger();

builder.WebHost.UseUrls("http://localhost:5005");

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


services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("User");
    });
});

services.AddAutoMapper(typeof(ReceiptProfile).Assembly);
services.AddBrokers(configuration);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Custom";
    options.DefaultChallengeScheme = "Custom";
    options.DefaultForbidScheme = "Custom";
})
.AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Custom", null);

services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IMongoInitializer>();
    await initializer.InitializeAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.AddMiddlewares();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Service API");
    options.RoutePrefix = "";
});

app.Run();

public partial class Program { }