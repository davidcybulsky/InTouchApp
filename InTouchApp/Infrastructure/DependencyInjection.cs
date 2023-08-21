using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using InTouchApi.Infrastructure.Data;
using InTouchApi.Infrastructure.Data.Repositories;
using InTouchApi.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiContext>(builder =>
                    builder.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
