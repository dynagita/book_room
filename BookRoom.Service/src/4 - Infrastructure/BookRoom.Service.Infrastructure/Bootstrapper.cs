using BookRoom.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Queue;
using BookRoom.Service.Domain.Repositories;
using BookRoom.Service.Infrastructure.Rabbit.Consumers;
using BookRoom.Service.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Service.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddConfiguration(configuration)
                .AddRepositories()
                .AddConsumers();
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitConfiguration>((rabbitConfiguration) =>
            {
                configuration.Bind(nameof(RabbitConfiguration), rabbitConfiguration);
            });
            services.Configure<MongoConfiguration>((mongoConfiguration) =>
            {
                configuration.Bind(nameof(MongoConfiguration), mongoConfiguration);
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IBookRoomRepository, BookRoomRepository>();
            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddTransient<IUserConsumer, UserConsumer>();
            services.AddTransient<IRoomConsumer, RoomConsumer>();
            services.AddTransient<IBookRoomConsumer, BookRoomConsumer>();
            return services;
        }
    }
}
