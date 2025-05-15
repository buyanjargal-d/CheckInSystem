using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    public interface ISeatAssignmentService
    {
        //bool LockSeat(int seatId);
        Task<bool> AssignSeatAsync(int passengerId, int seatId);

        //void UnlockSeat(int seatId);

        List<SeatDto> GetAvailableSeats(int flightId);
        List<SeatDto> GetAllSeats(int flightId);

        SeatDto? GetSeatByPassenger(int passengerId);


    }
}
