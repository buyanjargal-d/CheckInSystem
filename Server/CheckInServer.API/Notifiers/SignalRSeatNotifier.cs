using CheckInSystem.DTO;
using CheckInSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using CheckInSystem.Business.Interfaces;

namespace CheckInServer.API.Notifiers;

public class SignalRSeatNotifier : ISeatNotifier
{
    private readonly IHubContext<SeatHub> _hub;

    public SignalRSeatNotifier(IHubContext<SeatHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifySeatAssignedAsync(SeatDto seat)
    {
        Console.WriteLine("📡 SignalR: Sending SeatAssigned via hub...");
        await _hub.Clients.All.SendAsync("SeatAssigned", seat);
    }
}

