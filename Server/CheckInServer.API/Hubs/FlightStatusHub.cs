namespace CheckInSystem.Hubs
{
    using CheckInSystem.DTO;
    using Microsoft.AspNetCore.SignalR;

    public class FlightStatusHub : Hub
    {
        public async Task NotifyStatus(FlightDto flight)
        {
            await Clients.All.SendAsync("FlightStatusChanged", flight);
        }
    }
}
