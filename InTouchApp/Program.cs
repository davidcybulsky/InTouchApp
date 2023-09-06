using InTouchApi.Application;
using InTouchApi.Infrastructure;
using InTouchApi.Presentation;
using InTouchApi.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);

builder.Services.AddAplication(configuration)
                .AddInfrastructure(configuration)
                .AddPresentation(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => opt
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins(configuration["Cors:Client"]));

//if (!app.Environment.IsDevelopment())
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();