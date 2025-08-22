using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogService.API.Filters
{
    public class LogResultFilter : IAsyncResourceFilter
    {
        private readonly ILogger<LogResultFilter> _logger;
        public LogResultFilter(ILogger<LogResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            await next();

            var data = new
            {
                Action = context.ActionDescriptor.RouteValues["action"],
                Controller = context.ActionDescriptor.RouteValues["controller"],
                Url = context.HttpContext.Request.Path,
                context.HttpContext.Response.StatusCode
            };
            _logger.LogInformation("Request {@RequestData}", data);
        }
    }
}
