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
    }
}
