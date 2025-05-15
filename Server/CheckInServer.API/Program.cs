using CheckInSystem.Data;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Repositories;
using CheckInSystem.Business.Interfaces;
using CheckInSystem.Business.Services;
using Microsoft.EntityFrameworkCore;
using CheckInServer.API.Notifiers;
using CheckInSystem.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CheckInDbContext>(options =>
    options.UseSqlite("Data Source=checkin.db"));

builder.Services.AddScoped<ISeatAssignmentService, SeatAssignmentService>();
builder.Services.AddScoped<IFlightNotifier, SignalRFlightNotifier>();
builder.Services.AddScoped<ISeatNotifier, SignalRSeatNotifier>();
builder.Services.AddScoped<ISocketNotifier, SocketNotifier>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IFlightStatusService, FlightStatusService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

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

var app = builder.Build();

app.UseRouting();
app.UseCors(); 
app.UseAuthorization();

app.MapControllers();
app.MapHub<FlightStatusHub>("/hub/flight-status");
app.MapHub<SeatHub>("/hub/seat-updates");

app.Run();
