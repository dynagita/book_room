using BookRoom.Application.IoC;
using BookRoom.Infrastructure.Database.Context;
using BookRoom.WebApi.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostBuilder, configurationBuilder) =>
{
    configurationBuilder
    .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json");
});

Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()        
        .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo
            .Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
            })
        .WriteTo.Console()
        .CreateLogger();


builder.Services.AddSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddBootstrap(builder.Configuration);
builder.Services.AddBasicSecurity(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationHealthChecks(builder.Configuration);

var provider = builder.Services.BuildServiceProvider();

Log.Logger.Information("Application Starting");

using (var context = provider.GetService<BookRoomDbContext>())
{
    context.Database.Migrate();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.AddSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(appContext =>
{
    appContext.AllowAnyHeader();
    appContext.AllowAnyOrigin();
    appContext.AllowAnyMethod();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();

    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        Predicate = (check) => !check.Tags.Contains("services"),
        AllowCachingResponses = false        
    });
    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
    {
        Predicate = (check) => true,
        AllowCachingResponses = false,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });
});

app.Run();
