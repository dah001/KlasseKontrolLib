using KlasseLib.KlasseKontrolServices;
using KlasseLib.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your services for dependency injection

builder.Services.AddScoped<IClassRoom, ClassRoomDb>();
builder.Services.AddScoped<ISensorDB, SensorDB>();      // If you have any other services


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public interface IWeatherService
{
}