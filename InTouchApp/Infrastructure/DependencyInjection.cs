using FluentValidation;
using FluentValidation.AspNetCore;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using InTouchApi.Infrastructure.Data;
using InTouchApi.Infrastructure.Data.Repositories;
using InTouchApi.Infrastructure.Services;
using InTouchApi.Infrastructure.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace InTouchApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiContext>(builder =>
                    builder.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IReactionService, ReactionService>();
            services.AddScoped<IUserHttpContextService, UserHttpContextService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IReactionRepository, ReactionRepository>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true
                };
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Infrastructure/Logging/log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            services.AddFluentValidationAutoValidation().AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddScoped<IValidator<SignUpDto>, SignUpDtoValidator>();
            services.AddScoped<IValidator<UpdatePasswordDto>, UpdatePasswordDtoValidator>();
            services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();

            return services;
        }
    }
}
