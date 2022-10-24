using BookRoom.Readness.Domain.Contract.Configurations;
using BookRoom.Readness.Domain.Repositories;
using BookRoom.Readness.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Readness.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddConfiguration(configuration)
                .AddRepositories();
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfiguration>((authConfiguration) =>
            {
                configuration.Bind(nameof(AuthenticationConfiguration), authConfiguration);
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
    }
}
