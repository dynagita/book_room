using BookRoom.Application.IoC;
using BookRoom.Infrastructure.Database.Context;
using BookRoom.WebApi.Extensions;
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

var provider = builder.Services.BuildServiceProvider();

using (var context = provider.GetService<BookRoomDbContext>())
{
    context.Database.Migrate();
}

var app = builder.Build();

app.AddSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(appContext => {
    appContext.AllowAnyHeader();
    appContext.AllowAnyOrigin();
    appContext.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
