using InTouchApi.Presentation.Middlewares;

namespace InTouchApi.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddScoped<ExceptionHandlingMiddleware>();

            services.AddCors();

            return services;
        }
    }
}
