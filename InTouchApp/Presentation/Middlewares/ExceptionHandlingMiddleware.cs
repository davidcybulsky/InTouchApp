using InTouchApi.Application.Exceptions;
using Serilog;

namespace InTouchApi.Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(e.Message);
                Log.Error($"Error: {e.LoggerMessage}");
            }
            catch (UnauthorizedException e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(e.Message);
                Log.Error($"Error: {e.LoggerMessage}");
            }
            catch (ForbiddenException e)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync(e.Message);
                Log.Error($"Error: {e.LoggerMessage}");
            }
            catch (NotFoundException e)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(e.Message);
                Log.Error($"Error: {e.LoggerMessage}");
            }
            catch (NotImplementedException e)
            {
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;
                await context.Response.WriteAsync("The feature is not implemented");
                Log.Logger.Error($"Error: {e.Message}");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong");
                Log.Logger.Error($"Error: {e.Message}");
            }
        }
    }
}
