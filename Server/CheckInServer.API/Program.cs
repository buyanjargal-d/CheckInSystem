using CheckInSystem.Business.Interfaces;
using CheckInSystem.Business.Services;
using CheckInSystem.Data.Interfaces;

using CheckInServer.API.Notifiers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register EF Core with SQLite
builder.Services.AddDbContext<CheckInDbContext>(options =>
    options.UseSqlite("Data Source=checkin.db"));

// ✅ Register application services
builder.Services.AddScoped<ISeatAssignmentService, SeatAssignmentService>();
builder.Services.AddScoped<IFlightNotifier, SignalRFlightNotifier>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();


// ✅ Register controllers and SignalR
builder.Services.AddControllers();
builder.Services.AddSignalR();

// ✅ Build app
var app = builder.Build();

// ✅ Enable routing and authorization middleware
app.UseRouting();
app.UseAuthorization();

// ✅ Map endpoints
app.MapControllers();
//app.MapHub<FlightHub>("/hub"); // Optional: replace or remove if not using this hub

// ✅ Run the application
app.Run();
