using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Api.Extentions;
using IdentityService.API.Extentions;
using IdentityService.API.grpc;
using IdentityService.BLL.Options;
using IdentityService.BLL.Validation;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


builder.Host.UseSerilog();

services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001, o => o.Protocols = HttpProtocols.Http1);
    options.ListenAnyIP(5002, o => o.Protocols = HttpProtocols.Http2);
});

services.AddControllers();

builder.ConfigureOptions();
builder.AddDatabases();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Identity Service API",
    });
});

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddRepositories();

services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
services.AddFluentValidationAutoValidation();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddServices();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.AddMiddlewares();

app.SeedDatabase();

app.MapControllers();

app.MapGrpcService<TokenValidatorService>();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API");
    options.RoutePrefix = "swagger";
});

app.Run();

public partial class Program { }