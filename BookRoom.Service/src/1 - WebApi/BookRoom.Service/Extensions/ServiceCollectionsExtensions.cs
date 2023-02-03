using BookRoom.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Contract.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace BookRoom.Service.Extensions
{
    public static class ServiceCollectionsExtensions
    {       
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "BookRoom Service",
                        Version = "v1",
                        Description = "API used for hosting services to propagate data for the best hotel reservations!",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Yanagita",
                            Url = new Uri("https://www.linkedin.com/in/daniel-yanagita-88860770/"),
                            Email = "dynagita@gmail.com"
                        }
                    });                                
            });
            return services;
        }
        public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfig = new MongoConfiguration();
            configuration.Bind(nameof(MongoConfiguration), mongoConfig);

            var rabbitConfig = new RabbitConfiguration();
            configuration.Bind(nameof(RabbitConfiguration), rabbitConfig);

            services
                .AddHealthChecks()
                .AddMongoDb(mongoConfig.ConnectionString,
                           name: "Database",
                           failureStatus: HealthStatus.Unhealthy,
                           tags: new string[] { "service" },
                           timeout: TimeSpan.FromSeconds(15))
                .AddRabbitMQ(rabbitConfig.Connection,
                            name: "RabbitMQ",
                            failureStatus: HealthStatus.Unhealthy,
                            tags: new string[] { "service" },
                            timeout: TimeSpan.FromSeconds(15));
            return services;
        }
    }
}
