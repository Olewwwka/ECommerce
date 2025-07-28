using IdentityService.API.Extentions;
using IdentityService.BLL.Options;
using IdentityService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();

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

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService");
    c.RoutePrefix = "";
});

app.MapControllers();

app.Run();

