using BookRoom.Service.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookRoom.Service.Application.IoC
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddInfrastructure(configuration)
                .AddApplication();
        }
    }
}
