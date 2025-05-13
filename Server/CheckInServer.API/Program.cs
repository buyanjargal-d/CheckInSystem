using CheckInSystem.Business.Interfaces;
using CheckInServer.API.Notifiers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Register the notifier
builder.Services.AddScoped<IFlightNotifier, SignalRFlightNotifier>();

var app = builder.Build();

// Additional configuration for the app can go here

await app.RunAsync();
