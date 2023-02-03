using BookRoom.Service.Application.IoC;
using BookRoom.Service.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostBuilder, configurationBuilder) =>
{
    configurationBuilder
    .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment.EnvironmentName}.json");
});

builder.Services.AddBootstrap(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationHealthChecks(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.AddSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

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
