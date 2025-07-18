using System.Text.Json;
using IdentityService.BLL.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdentityService.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                _logger.LogWarning(ex, ex.Message);
            }
            catch (RefreshTokenExpiredException ex)
            {
                await HandleError(context, StatusCodes.Status409Conflict, ex.Message);
                _logger.LogWarning(ex, ex.Message);
            }
            catch (RefreshTokenNotFoundException ex)
            {
                await HandleError(context, StatusCodes.Status404NotFound, ex.Message);
                _logger.LogWarning(ex, ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                await HandleError(context, StatusCodes.Status409Conflict, ex.Message);
                _logger.LogWarning(ex, ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                await HandleError(context, StatusCodes.Status404NotFound, ex.Message);
                _logger.LogWarning(ex, ex.Message);
            }
            catch(InvalidAccessTokenException ex)
            {
                await HandleError(context, StatusCodes.Status401Unauthorized, ex.Message);
                _logger.LogWarning(ex, ex.Message);
            }
            catch(Exception ex)
            {
                await HandleError(context, StatusCodes.Status401Unauthorized, "Invalid server error =(");
                _logger.LogCritical(ex, ex.Message);
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
