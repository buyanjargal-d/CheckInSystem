using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;

namespace CheckInSystem.Hubs
{
    /// <summary>
    /// FlightStatusHub нь SignalR ашиглан нислэгийн төлөвийн өөрчлөлтийг 
    /// бүх холбогдсон клиентүүдэд мэдэгдэх зориулалттай хаб юм.
    /// </summary>
    public class FlightStatusHub : Hub
    {
        /// <summary>
        /// NotifyStatus нь нислэгийн шинэчлэгдсэн мэдээллийг бүх клиентүүдэд илгээдэг.
        /// </summary>
        /// <param name="flight">Шинэчлэгдсэн нислэгийн мэдээлэл (FlightDto)</param>
        public async Task NotifyStatus(FlightDto flight)
        {
            // "FlightStatusChanged" нэртэй эвентээр бүх клиент рүү нислэгийн шинэ мэдээллийг илгээнэ
            await Clients.All.SendAsync("FlightStatusChanged", flight);
        }
    }
}
