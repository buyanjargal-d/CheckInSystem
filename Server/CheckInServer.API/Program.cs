using CheckInSystem.Data;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Repositories;
using CheckInSystem.Business.Interfaces;
using CheckInSystem.Business.Services;
using Microsoft.EntityFrameworkCore;
using CheckInServer.API.Notifiers;
using CheckInSystem.Hubs;

// Вэб програмын бүтээгчийг үүсгэнэ
var builder = WebApplication.CreateBuilder(args);

// Өгөгдлийн сангийн контекстийг Sqlite ашиглан бүртгэнэ
builder.Services.AddDbContext<CheckInDbContext>(options =>
    options.UseSqlite("Data Source=checkin.db"));

// Бизнесийн болон дата давхаргын үйлчилгээнүүдийг бүртгэнэ
builder.Services.AddScoped<ISeatAssignmentService, SeatAssignmentService>();
builder.Services.AddScoped<IFlightNotifier, SignalRFlightNotifier>();
builder.Services.AddScoped<ISeatNotifier, SignalRSeatNotifier>();
builder.Services.AddScoped<ISocketNotifier, SocketNotifier>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IFlightStatusService, FlightStatusService>();

// Контроллер болон SignalR-ийг бүртгэнэ
builder.Services.AddControllers();
builder.Services.AddSignalR();

// CORS тохиргоог бүртгэнэ (бүх origin-ийг зөвшөөрнө)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Програмыг үүсгэнэ
var app = builder.Build();

// Middleware-уудыг тохируулна
app.UseRouting();
app.UseCors(); 
app.UseAuthorization();

// Контроллер болон SignalR hub-уудыг замчилна
app.MapControllers();
app.MapHub<FlightStatusHub>("/hub/flight-status");
app.MapHub<SeatHub>("/hub/seat-updates");

// Програмыг ажиллуулна
app.Run();
