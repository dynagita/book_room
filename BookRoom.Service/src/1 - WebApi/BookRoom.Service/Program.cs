using BookRoom.Service.Application.IoC;
using BookRoom.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBootstrap(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

app.AddSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
