using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;

namespace CheckInSystem.Hubs
{
    public class FlightStatusHub : Hub
    {
        public async Task NotifyStatus(FlightDto flight)
        {
            await Clients.All.SendAsync("FlightStatusChanged", flight);
        }
    }
}
