using CheckInSystem.DTO;

namespace CheckInSystem.Business.Interfaces;

/// <summary>
/// Сокет мэдэгдэл илгээгч интерфейс.
/// Энэ интерфейс нь суудал оноогдсон тухай болон бусад суудлын төлөвийн өөрчлөлтийн талаар
/// сокет ашиглан мэдэгдэл илгээхэд ашиглагдана.
/// </summary>
public interface ISocketNotifier
{
    /// <summary>
    /// Суудал оноогдсон тухай мэдэгдэл илгээнэ.
    /// </summary>
    /// <param name="seatId">Суудлын дугаар</param>
    /// <param name="passengerId">Зорчигчийн дугаар</param>
    void NotifySeatAssigned(int seatId, int passengerId);
    //void NotifySeatLocked(int seatId);
    //void NotifySeatUnlocked(int seatId);
}
