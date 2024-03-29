﻿using BookRoom.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookRoom.Application.IoC
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