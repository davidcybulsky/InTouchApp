using InTouchApi.Application.Exceptions;

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
            }
            catch (UnauthorizedException e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(e.Message);
            }
            catch (ForbiddenException e)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync(e.Message);
            }
            catch (NotFoundException e)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(e.Message);
            }
            catch (NotImplementedException)
            {
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;
                await context.Response.WriteAsync("The feature is not implemented");
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
