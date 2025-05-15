using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace CheckInSystem.Data.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CheckInDbContext _context;

        public SeatRepository(CheckInDbContext context)
        {
            _context = context;
        }

        public List<SeatDto> GetAvailableSeats(int flightId)
        {
            return _context.Seats
                .Where(s => s.FlightId == flightId && !s.IsOccupied)
                .Select(s => new SeatDto
                {
                    SeatId = s.SeatId,
                    SeatNumber = s.SeatNumber,
                    IsOccupied = s.IsOccupied,
                    PassengerId = s.PassengerId
                })
                .ToList();
        }

        public bool AssignSeat(int passengerId, int seatId)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);
            if (seat == null || seat.IsOccupied) return false;

            seat.IsOccupied = true;
            seat.PassengerId = passengerId;
            _context.SaveChanges();
            return true;
        }

        //public bool LockSeat(int seatId)
        //{
        //    var seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);
        //    if (seat == null || seat.IsOccupied) return false;

        //    seat.IsOccupied = true;
        //    _context.SaveChanges();
        //    return true;
        //}

        //public void UnlockSeat(int seatId)
        //{
        //    var seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);
        //    if (seat != null)
        //    {
        //        seat.IsOccupied = false;
        //        seat.PassengerId = null;
        //        _context.SaveChanges();
        //    }
        //}

        public bool IsAvailable(int seatId)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);
            return seat != null && !seat.IsOccupied;
        }

        public IEnumerable<object> GetSeatsByFlightId(int flightId)
        {
            return _context.Seats
                .Where(s => s.FlightId == flightId)
                .Select(s => new
                {
                    s.SeatId,
                    s.SeatNumber,
                    s.IsOccupied,
                    s.PassengerId
                })
                .ToList();
        }

        public IEnumerable<SeatDto> GetAllSeatsByFlightId(int flightId)
        {
            return _context.Seats
                .Where(s => s.FlightId == flightId)
                .Select(s => new SeatDto
                {
                    SeatId = s.SeatId,
                    SeatNumber = s.SeatNumber,
                    IsOccupied = s.IsOccupied,
                    PassengerId = s.PassengerId
                })
                .ToList();
        }

        public SeatDto? GetSeatByPassengerId(int passengerId)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.PassengerId == passengerId);
            return seat == null ? null : new SeatDto
            {
                SeatId = seat.SeatId,
                SeatNumber = seat.SeatNumber,
                IsOccupied = seat.IsOccupied,
                PassengerId = seat.PassengerId
            };
        }

    }
}
