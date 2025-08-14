using CatalogService.Domain.Exceptions;

namespace CatalogService.API.Middlewares
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

            switch (exception)
            {
                case NotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    message = "Resource not found:" + exception.Message;
                    _logger.LogWarning(exception, message);
                    break;
                case AlreadyExistsException:
                    response.StatusCode = StatusCodes.Status409Conflict;
                    message = "This thing already exists! " + exception.Message;
                    _logger.LogWarning(exception, message);
                    break;
                case CategoryMismatchException:
                    response.StatusCode = StatusCodes.Status409Conflict;
                    message = "Category mismath! " + exception.Message;
                    _logger.LogWarning(exception, message);
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
