using BookRoom.Application.Handlers.Commands.UserHandlers;
using BookRoom.Application.Handlers.Queries.AuthQueries;
using BookRoom.Application.Mappers;
using BookRoom.Application.UseCases.Auth;
using BookRoom.Application.UseCases.UserUseCases;
using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Contract.UseCases.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Application
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddConfigurations(configuration)
                .AddProfiles()
                .AddUseCases()
                .AddHandlers();
        }

        public static IServiceCollection AddProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile)
                );
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(AuthHandler),
                typeof(UserCreateHandler)
                );
            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateUseCase, AuthenticateUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<ITokenCreateUseCase, TokenCreateUseCase>();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((serviceProvider) => {
                var authConfig = new AuthenticationConfiguration();
                configuration.Bind(nameof(AuthenticationConfiguration), authConfig);
                return authConfig;
            });
            return services;
        }
    }
}
