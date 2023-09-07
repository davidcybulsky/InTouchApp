using InTouchApi.Application.Authorization;
using InTouchApi.Application.Authorization.Handlers;
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

            services.AddScoped<IAuthorizationHandler, EditPostCommentRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, DeletePostCommentRequirementHandler>();

            services.AddScoped<IAuthorizationHandler, EditOrDeletePostReactionRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, EditOrDeleteCommentReactionRequirementHandler>();

            return services;
        }
    }
}
