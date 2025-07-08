using OrderService.API.Extentions;
using OrderService.Domain.Abstractions.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configurations = builder.Configuration;

builder.ConfigureOptions();
builder.AddDb();

services.AddRepositories();
services.AddServices();

var app = builder.Build();

var initializer = app.Services.GetRequiredService<IDbInitializer>();
await initializer.InitializeDbAsync();

app.Run();

