using OrderService.Domain.Exceptions;

namespace OrderService.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
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

            switch (exception)
            {
                case NotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    message = "Resource not found:" + exception.Message;
                    _logger.LogWarning(exception, message);
                    break;
                case UserHasUnpaidOrderException:
                    response.StatusCode = StatusCodes.Status409Conflict;
                    message = "Confilct:" + exception.Message;
                    _logger.LogWarning(exception, message);
                    break;
                case Exception:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Unhandled exception" + exception.Message;
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
