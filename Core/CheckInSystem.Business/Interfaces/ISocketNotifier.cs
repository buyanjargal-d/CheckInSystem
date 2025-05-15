using CheckInSystem.DTO;

namespace CheckInSystem.Business.Interfaces;

public interface ISocketNotifier
{
    void NotifySeatAssigned(int seatId, int passengerId);
    //void NotifySeatLocked(int seatId);
    //void NotifySeatUnlocked(int seatId);
}
