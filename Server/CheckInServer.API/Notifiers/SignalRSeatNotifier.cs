using CheckInSystem.DTO;
using CheckInSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using CheckInSystem.Business.Interfaces;

namespace CheckInServer.API.Notifiers;

/// <summary>
/// SignalRSeatNotifier нь суудал оноох үед SignalR ашиглан бүх клиентүүдэд мэдэгдэл илгээдэг notifier класс юм.
/// Энэ класс нь ISeatNotifier интерфейсийг хэрэгжүүлж, суудал оноосон тухай мэдээллийг бодит цаг дээр дамжуулна.
/// </summary>
public class SignalRSeatNotifier : ISeatNotifier
{
    private readonly IHubContext<SeatHub> _hub;

    /// <summary>
    /// SignalRSeatNotifier классын конструктор.
    /// </summary>
    /// <param name="hub">SeatHub-ийн SignalR hub context объект.</param>
    public SignalRSeatNotifier(IHubContext<SeatHub> hub)
    {
        _hub = hub;
    }

    /// <summary>
    /// Суудал оноосон тухай бүх клиентүүдэд бодит цаг дээр мэдэгдэл илгээх арга.
    /// </summary>
    /// <param name="seat">Оноосон суудлын мэдээлэл агуулсан SeatDto объект.</param>
    public async Task NotifySeatAssignedAsync(SeatDto seat)
    {
        Console.WriteLine("📡 SignalR: Sending SeatAssigned via hub...");
        await _hub.Clients.All.SendAsync("SeatAssigned", seat);
    }
}

