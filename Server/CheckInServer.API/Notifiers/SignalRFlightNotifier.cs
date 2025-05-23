using CheckInSystem.Business.Interfaces;
using Microsoft.AspNetCore.SignalR;
using CheckInSystem.DTO;
using CheckInSystem.Hubs;

/// <summary>
/// SignalRFlightNotifier нь нислэгийн төлөвийн өөрчлөлтийг SignalR ашиглан
/// бүх холбогдсон клиентүүдэд мэдэгдэх зориулалттай хэрэгжүүлэлт юм.
/// Энэ класс нь IFlightNotifier интерфейсийг хэрэгжүүлж, 
/// нислэгийн ID болон төлөвийг бүх клиентүүдэд илгээдэг.
/// </summary>
public class SignalRFlightNotifier : IFlightNotifier
{
    private readonly IHubContext<FlightStatusHub> _hub;

    /// <summary>
    /// Шинэ SignalRFlightNotifier объект үүсгэнэ.
    /// </summary>
    /// <param name="hub">FlightStatusHub-ийн SignalR контекст</param>
    public SignalRFlightNotifier(IHubContext<FlightStatusHub> hub)
    {
        _hub = hub;
    }

    /// <summary>
    /// Нислэгийн төлөвийн өөрчлөлтийг бүх клиентүүдэд мэдэгдэнэ.
    /// </summary>
    /// <param name="flightId">Нислэгийн давтагдашгүй дугаар (ID)</param>
    /// <param name="status">Нислэгийн шинэ төлөв</param>
    public async Task NotifyFlightStatusAsync(int flightId, string status)
    {
        try
        {
            Console.WriteLine($"SignalR: Sending notification for flight {flightId} with status {status}");

            var message = new
            {
                FlightId = flightId,
                Status = status
            };

            await _hub.Clients.All.SendAsync("FlightStatusChanged", message);
            Console.WriteLine($"SignalR: Successfully sent notification for flight {flightId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SignalR Error: {ex.Message}");
            throw;
        }
    }
}