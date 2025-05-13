using CheckInSystem.DTO;
using Microsoft.AspNetCore.SignalR;

namespace CheckInServer.API.Hubs
{
    public class SeatHub : Hub
    {
        public async Task NotifySeatAssigned(SeatDto seat)
        {
            await Clients.All.SendAsync("SeatAssigned", seat);
        }
    }
}
