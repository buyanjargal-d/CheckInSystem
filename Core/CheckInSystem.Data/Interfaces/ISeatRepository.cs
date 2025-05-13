using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Data.Interfaces
{
    public interface ISeatRepository
    {
        List<SeatDto> GetAvailableSeats(int flightId);
        bool AssignSeat(int passengerId, int seatId);
        bool LockSeat(int seatId);
        void UnlockSeat(int seatId);
        bool IsAvailable(int seatId);
        IEnumerable<object> GetSeatsByFlightId(int flightId);
    }
}
