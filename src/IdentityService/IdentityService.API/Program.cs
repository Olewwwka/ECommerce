using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.API.Extentions;
using IdentityService.BLL.Validation;
using IdentityService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication;
using IdentityService.API.Handlers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
services.AddFluentValidationAutoValidation();

services.AddAuthentication("CustomAuth")
    .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("CustomAuth", options => { });

services.AddAuthorization();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IdentityService"
    });
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddServices();

services.AddDbContext<IdentityServiceDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(IdentityServiceDbContext)));
});

services.AddSingleton<IConnectionMultiplexer>(cm =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

builder.ConfigureOptions();

var app = builder.Build();

app.AddMiddlewares();

await DatabaseInitializer.InitializeRolesAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService");
    c.RoutePrefix = "";
});

app.UseAuthorization();
app.MapControllers();

app.Run();

