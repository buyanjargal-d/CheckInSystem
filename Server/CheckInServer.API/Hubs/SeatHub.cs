using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;

namespace CheckInSystem.Hubs
{
    /// <summary>
    /// SeatHub нь SignalR ашиглан суудлын мэдээллийг бодит цаг дээр дамжуулах зориулалттай хаб юм.
    /// Энэ хаб нь суудал оноогдсон тухай мэдээллийг бүх холбогдсон клиентүүдэд илгээдэг.
    /// </summary>
    public class SeatHub : Hub
    {
        /// <summary>
        /// Суудал оноогдсон тухай мэдээллийг бүх клиентүүдэд илгээх арга.
        /// </summary>
        /// <param name="seat">Оноогдсон суудлын мэдээлэл агуулсан SeatDto объект.</param>
        public async Task NotifySeatAssigned(SeatDto seat)
        {
            // Бүх холбогдсон клиентүүдэд "SeatAssigned" нэртэй эвентээр суудлын мэдээллийг илгээх
            await Clients.All.SendAsync("SeatAssigned", seat);
        }
    }
}
