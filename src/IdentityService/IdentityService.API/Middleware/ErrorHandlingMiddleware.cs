using System.Text.Json;
using IdentityService.BLL.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityService.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (IncorrectPasswordException ex)
            {
                await HandleError(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (RefreshTokenExpiredException ex)
            {
                await HandleError(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (RefreshTokenNotFoundException ex)
            {
                await HandleError(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                await HandleError(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                await HandleError(context, StatusCodes.Status404NotFound, ex.Message);
            }
        }

        private static async Task HandleError(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = JsonSerializer.Serialize(message);

            await context.Response.WriteAsync(response);
        }
    }
}
