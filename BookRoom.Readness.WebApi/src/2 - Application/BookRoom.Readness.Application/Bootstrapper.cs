using BookRoom.Readness.Application.Handlers.Queries.AuthQueries;
using BookRoom.Readness.Application.Handlers.Queries.BookRoomsHandlers;
using BookRoom.Readness.Application.Handlers.Queries.RoomHandlers;
using BookRoom.Readness.Application.Profiles;
using BookRoom.Readness.Application.UseCases.Auth;
using BookRoom.Readness.Application.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Application.UseCases.RoomUseCases;
using BookRoom.Readness.Domain.Contract.UseCases.Auth;
using BookRoom.Readness.Domain.Contract.UseCases.BookRoomsUseCases;
using BookRoom.Readness.Domain.Contract.UseCases.RomUseCases;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BookRoom.Readness.Application
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
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
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(AuthHandler).Assembly,
                typeof(ListRoomRequestHandler).Assembly,
                typeof(ListBooksByUserRequestHandler).Assembly,
                typeof(CheckAvailabilityRequestHandler).Assembly
            };
            services.AddMediatR(assemblies);
            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticateUseCase, AuthenticateUseCase>();
            services.AddTransient<ITokenCreateUseCase, TokenCreateUseCase>();
            services.AddTransient<IListRoomUseCase, ListRoomUseCase>();
            services.AddTransient<IListBooksByUserUseCase, ListBooksByUserUseCase>();
            services.AddTransient<ICheckAvailabilityUseCase, CheckAvailabilityUseCase>();
            return services;
        }
    }
}
