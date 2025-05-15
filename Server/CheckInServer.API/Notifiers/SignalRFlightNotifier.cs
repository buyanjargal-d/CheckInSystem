using CheckInSystem.Business.Interfaces;
using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;
using CheckInSystem.Hubs;

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
