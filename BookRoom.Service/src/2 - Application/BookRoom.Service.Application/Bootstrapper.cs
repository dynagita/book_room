using BookRoom.Service.Application.HostedServices;
using BookRoom.Service.Application.Profiles;
using BookRoom.Service.Application.UseCases.BookRoomPropagation;
using BookRoom.Service.Application.UseCases.RoomPropagation;
using BookRoom.Service.Application.UseCases.UserPropagation;
using BookRoom.Service.Domain.Contract.UseCases;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookRoom.Service.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddMappers()
                .AddUseCases()
                .AddHandlers()
                .AddHostedServices();
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IUpdateUserBookRoomUseCase, UpdateUserBookRoomUseCase>();
            services.AddTransient<IUpdateRoomBookRoomUseCase, UpdateRoomBookRoomUseCase>();
            services.AddTransient<IUpdateBookRoomInUserUseCase, UpdateBookRoomInUserUseCase>();
            services.AddTransient<IUpdateBookRoomInRoomUseCase, UpdateBookRoomInRoomUseCase>();

            services.AddTransient<IPropagateUserUseCase, PropagateUserUseCase>();
            services.AddTransient<IPropagateRoomUseCase, PropagateRoomUseCase>();
            services.AddTransient<IPropagateBookRoomUseCase, PropagateBookRoomUseCase>();

            return services;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<ProcessUserService>();
            services.AddHostedService<ProcessRoomService>();
            services.AddHostedService<ProcessBookRoomService>();
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly()                
            };
            services.AddMediatR(assemblies);
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(BookRoomProfile),
                typeof(RoomProfile),
                typeof(UserProfile)
                );
            return services;
        }
    }
}
