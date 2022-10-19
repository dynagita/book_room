using BookRoom.Application.Handlers.Commands.User;
using BookRoom.Application.Handlers.Queries.Auth;
using BookRoom.Application.Mappers;
using BookRoom.Application.UseCases.Auth;
using BookRoom.Application.UseCases.User;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Contract.UseCases.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Application
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
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
            return services;
        }
    }
}
