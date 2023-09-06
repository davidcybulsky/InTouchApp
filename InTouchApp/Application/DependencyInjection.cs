using InTouchApi.Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace InTouchApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorizationHandler, AcceptFriendRequestRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, EditPostRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, DeletePostRequirementHandler>();

            return services;
        }
    }
}
