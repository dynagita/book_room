using BookRoom.Readness.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookRoom.Readness.Application.IoC
{
    public static class Bootstrap
    {
        public static IServiceCollection AddBootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddApplication(configuration);

            return services;
        }
    }
}