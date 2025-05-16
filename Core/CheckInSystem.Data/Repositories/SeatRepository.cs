using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace CheckInSystem.Data.Repositories
{
    /// <summary>
    /// SeatRepository ангилал нь суудлын өгөгдлийн сангийн үйлдлүүдийг гүйцэтгэхэд ашиглагдана.
    /// Энэ ангилал нь нислэгийн суудлуудыг авах, оноох, шалгах зэрэг үндсэн үйлдлүүдийг хэрэгжүүлдэг.
    /// </summary>
    public class SeatRepository : ISeatRepository
    {
        private readonly CheckInDbContext _context;

        /// <summary>
        /// SeatRepository-ийн шинэ экземплярыг үүсгэнэ.
        /// </summary>
        /// <param name="context">Өгөгдлийн сангийн контекст</param>
        public SeatRepository(CheckInDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Тухайн нислэгийн боломжит (чөлөөтэй) суудлуудыг буцаана.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Суудлын DTO жагсаалт</returns>
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

        /// <summary>
        /// Суудлыг зорчигчид оноох.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <param name="seatId">Суудлын ID</param>
        /// <returns>Амжилттай оноосон эсэх</returns>
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
        //
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

        /// <summary>
        /// Суудал чөлөөтэй эсэхийг шалгана.
        /// </summary>
        /// <param name="seatId">Суудлын ID</param>
        /// <returns>Чөлөөтэй бол true</returns>
        public bool IsAvailable(int seatId)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);
            return seat != null && !seat.IsOccupied;
        }

        /// <summary>
        /// Нислэгийн бүх суудлыг буцаана (object төрөлтэй).
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Суудлын жагсаалт</returns>
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

        /// <summary>
        /// Нислэгийн бүх суудлыг SeatDto хэлбэрээр буцаана.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>SeatDto жагсаалт</returns>
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

        /// <summary>
        /// Зорчигчийн ID-аар суудлыг буцаана.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <returns>SeatDto эсвэл null</returns>
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
