using BookRoom.Application.IoC;
using BookRoom.Infrastructure.Database.Context;
using BookRoom.WebApi.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostBuilder, configurationBuilder) =>
{
    configurationBuilder
    .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json");
});

// Add services to the container.
builder.Services.AddBootstrap(builder.Configuration);
builder.Services.AddBasicSecurity(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationHealthChecks(builder.Configuration);

var provider = builder.Services.BuildServiceProvider();

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
