using CheckInSystem.Business.Interfaces;
using CheckInSystem.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CheckInServer.API.Notifiers;

public class SignalRFlightNotifier : IFlightNotifier
{
    private readonly IHubContext<FlightStatusHub> _hub;

    public SignalRFlightNotifier(IHubContext<FlightStatusHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifyFlightStatusAsync(int flightId, string status)
    {
        await _hub.Clients.All.SendAsync("FlightStatusChanged", new
        {
            FlightId = flightId,
            Status = status
        });
    }
}
