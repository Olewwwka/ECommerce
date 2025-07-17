using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Api.Extentions;
using IdentityService.API.Extentions;
using IdentityService.BLL.Options;
using IdentityService.BLL.Validation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddDatabases();

builder.ConfigureOptions();

services.AddApiAutorization(configuration,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());


services.AddControllers();

services.AddRepositories();

services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
services.AddFluentValidationAutoValidation();



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

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.AddMiddlewares();

app.SeedDatabase();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService");
    c.RoutePrefix = "";
});

app.MapControllers();

app.Run();

