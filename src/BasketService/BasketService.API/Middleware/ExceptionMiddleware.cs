using BasketService.Domain.Exceptions;
using StackExchange.Redis;

namespace BasketService.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await Handle(context, ex);
            }
        }

        private async Task Handle(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var message = string.Empty;
            
            switch(exception)
            {
                case NotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    message = "Resource not found:" +  exception.Message;
                    _logger.LogWarning(exception, message);
                    break;
                case RedisConnectionException:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Redis connection exception" +  exception.Message;
                    _logger.LogCritical(exception, message);
                    Console.WriteLine("done");
                    break;
                case RedisCommandException:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Redis command executing exception" + exception.Message;
                    _logger.LogCritical(exception, message);
                    break;
                case RedisException:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Redis Unhandled exception";
                    _logger.LogCritical(exception, message);
                    break;
                case Exception:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Unhandled Exception" + exception.Message;
                    _logger.LogCritical(exception, message);
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Internal server error =( " + exception.Message;
                    _logger.LogCritical(exception, message);
                    break;
            }

            await response.WriteAsync(message);
        }
    }
}
