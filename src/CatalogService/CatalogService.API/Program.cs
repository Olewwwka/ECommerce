using CatalogService.API.Extentions;
using CatalogService.API.Filters;
using CatalogService.Application.Features.ProductAttributes.Comands.Create;
using CatalogService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

builder.AddLogger();

builder.Host.UseSerilog();

services.AddControllers(options =>
    {
        options.Filters.Add<LogResultFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog Service API",
        Version = "v1",
    });
});

builder.Services.AddDbContext<CatalogServiceDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString(nameof(CatalogServiceDbContext))));


services.AddRepositories();
services.ConfigureServices();

builder.ConfigureOptions();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddValidatorsFromAssembly(typeof(CreateProductAttributeCommandValidator).Assembly);

services.AddMediatR(m =>
    m.RegisterServicesFromAssembly(typeof(CreateProductAttributeCommandValidator).Assembly));

var app = builder.Build();

app.AddMiddlewares();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service API");
    options.RoutePrefix = string.Empty; 
});

app.MapControllers();
app.Run();
