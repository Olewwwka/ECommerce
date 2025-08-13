using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.WebHost.UseUrls("http://localhost:5000");

builder.Configuration
    .AddJsonFile("Config/configuration.json", optional: false, reloadOnChange: true)
    .AddJsonFile("Config/ocelot.swagger.json", optional: false, reloadOnChange: true);

configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = "Config";
});


services.AddOcelot(configuration);
services.AddSwaggerForOcelot(builder.Configuration);

services.AddAuthorization();

var app = builder.Build();

app.UseAuthorization();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
}).UseOcelot().Wait();



app.Run();
