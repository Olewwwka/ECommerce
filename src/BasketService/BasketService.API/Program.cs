using BasketService.API.Extentions;

var builder = WebApplication.CreateBuilder(args);

var configurations = builder.Configuration;
var services = builder.Services;

builder.ConnectToDatabase();

services.AddRepositories();

var app = builder.Build();



app.Run();

