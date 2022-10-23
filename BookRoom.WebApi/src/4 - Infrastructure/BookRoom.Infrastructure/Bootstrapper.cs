using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Queue;
using BookRoom.Domain.Repositories.EntityFramework;
using BookRoom.Infrastructure.Database.Context;
using BookRoom.Infrastructure.Database.Repositories;
using BookRoom.Infrastructure.Rabbit.Consumers;
using BookRoom.Infrastructure.Rabbit.Producers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigurations(configuration);
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddProducers();
            services.AddConsumers();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContextPool<BookRoomDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("Postgre")));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IBookRoomsRepository, BookRoomsRepository>();
            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfiguration>((authConfig) =>
            {
                configuration.Bind(nameof(AuthenticationConfiguration), authConfig);
            });

            services.Configure<RabbitConfiguration>((rabbitConfig) =>
            {
                configuration.Bind(nameof(RabbitConfiguration), rabbitConfig);
            });

            return services;
        }

        public static IServiceCollection AddProducers(this IServiceCollection services)
        {
            services.AddTransient<IUserProducer, UserProducer>();
            services.AddTransient<IRoomProducer, RoomProducer>();
            services.AddTransient<IBookRoomProducer, BookRoomProducer>();
            services.AddTransient<IBookRoomRequestProducer, BookRoomRequestProducer>();
            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddTransient<IBookRoomRequestConsumer, BookRoomRequestConsumer>();
            return services;
        }
    }
}
