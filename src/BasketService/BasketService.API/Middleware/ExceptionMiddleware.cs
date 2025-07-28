using BasketService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BasketService.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Internal server error =( " + exception.Message;
                    break;
            }

            await response.WriteAsync(message);
        }
    }
}
