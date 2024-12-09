using KlasseLib.KlasseKontrolServices;
using KlasseLib.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register your services for dependency injection
builder.Services.AddScoped<IClassRoom, ClassRoomDb>();
builder.Services.AddScoped<ISensorDB, SensorDB>(); // If you have other services

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow your frontend to communicate with the backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()  // This allows any origin
            .AllowAnyMethod()  // This allows any method
            .AllowAnyHeader()); // This allows any header
});

var app = builder.Build();

// Enable Swagger UI in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAll");

// Enable HTTPS Redirection and Authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers to handle requests
app.MapControllers();

// Run the application
app.Run();