using BookRoom.Readness.Domain.Contract.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookRoom.Readness.WebApi.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddBasicSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = new AuthenticationConfiguration();
            configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authConfig);

            var secret = Encoding.ASCII.GetBytes(authConfig.AuthSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "BookRoom Readness",
                        Version = "v1",
                        Description = "API used to read data for best hotel reservations!",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Yanagita",
                            Url = new Uri("https://www.linkedin.com/in/daniel-yanagita-88860770/"),
                            Email = "dynagita@gmail.com"
                        }
                    });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

            });
            return services;
        }

        public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfig = new MongoConfiguration();
            configuration.Bind(nameof(MongoConfiguration), mongoConfig);

            services
                .AddHealthChecks()
                .AddMongoDb(mongoConfig.ConnectionString,
                           name: "Database",
                           failureStatus: HealthStatus.Unhealthy,
                           tags: new string[] { "service" },
                           timeout: TimeSpan.FromSeconds(15));                
            return services;
        }
    }
}
