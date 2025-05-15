using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;

namespace CheckInSystem.Hubs
{
    public class SeatHub : Hub
    {
        public async Task NotifySeatAssigned(SeatDto seat)
        {
            await Clients.All.SendAsync("SeatAssigned", seat);
        }
    }
}
