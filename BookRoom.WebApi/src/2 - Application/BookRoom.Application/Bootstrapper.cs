using BookRoom.Application.Handlers.Commands.BookRoomHandlers;
using BookRoom.Application.Handlers.Commands.RoomHandlers;
using BookRoom.Application.Handlers.Commands.UserHandlers;
using BookRoom.Application.Handlers.Queries.AuthQueries;
using BookRoom.Application.HostedServices.BookRoomRequest;
using BookRoom.Application.Mappers;
using BookRoom.Application.UseCases.Auth;
using BookRoom.Application.UseCases.BookRoomUseCases;
using BookRoom.Application.UseCases.RoomUseCases;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Application.UseCases.UserUseCases;
using BookRoom.Domain.Contract.UseCases.Auth;
using BookRoom.Domain.Contract.UseCases.BookRooms;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Contract.UseCases.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BookRoom.Application
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddProfiles()
                .AddUseCases()
                .AddHandlers()
                .AddServiceHosted();
        }

        public static IServiceCollection AddProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile),
                typeof(RoomProfile)
                );
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(AuthHandler).Assembly,
                typeof(UserCreateHandler).Assembly,
                typeof(CreateRoomHandler).Assembly,
                typeof(UpdateRoomHandler).Assembly,
                typeof(DeleteRoomHandler).Assembly,
                typeof(CancelBookRoomHandler).Assembly,
                typeof(CreateBookRoomHandler).Assembly,
                typeof(UpdateBookRoomHandler).Assembly
            };
            services.AddMediatR(assemblies);
            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IBookRoomValidationUseCase, BookRoomValidationUseCase>();
            services.AddTransient<IAuthenticateUseCase, AuthenticateUseCase>();
            services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
            services.AddTransient<ITokenCreateUseCase, TokenCreateUseCase>();
            services.AddTransient<ICreateRoomUseCase, CreateRoomUseCase>();
            services.AddTransient<IDeleteRoomUseCase, DeleteRoomUseCase>();
            services.AddTransient<IUpdateRoomUseCase, UpdateRoomUseCase>();
            services.AddTransient<ICancelBookRoomUseCase, CancelBookRoomUseCase>();
            services.AddTransient<ICreateBookRoomUseCase, CreateBookRoomUseCase>();
            services.AddTransient<IUpdateBookRoomUseCase, UpdateBookRoomUseCase>();
            services.AddTransient<IBookRoomProcessUseCase, BookRoomProcessUseCase>();
            return services;
        }

        public static IServiceCollection AddServiceHosted(this IServiceCollection services)
        {
            services.AddHostedService<BookRoomRequestService>();
            return services;
        }
    }
}
