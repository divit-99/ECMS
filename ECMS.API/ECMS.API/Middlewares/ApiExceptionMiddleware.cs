using ECMS.API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ECMS.API.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException vex)
            {
                _logger.LogWarning(vex, "Validation error");
                await WriteError(context, StatusCodes.Status400BadRequest, vex.Message + "!");
            }
            catch (DuplicateResourceException dex)
            {
                _logger.LogWarning(dex, "Duplicate resource error");
                await WriteError(context, StatusCodes.Status409Conflict, dex.Message + "!");
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Not found");
                await WriteError(context, StatusCodes.Status404NotFound, ex.Message + "!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteError(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred!");
            }
        }

        private Task WriteError(HttpContext ctx, int statusCode, string message)
        {
            ctx.Response.StatusCode = statusCode;
            ctx.Response.ContentType = "application/json";
            return ctx.Response.WriteAsJsonAsync(new { message });
        }
    }
}
