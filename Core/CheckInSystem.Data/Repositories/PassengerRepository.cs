using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;
using Microsoft.EntityFrameworkCore;

namespace CheckInSystem.Data.Repositories
{
    /// <summary>
    /// PassengerRepository нь зорчигчийн өгөгдлийн сангийн үйлдлүүдийг хэрэгжүүлдэг анги юм.
    /// Энэ анги нь зорчигчийн мэдээллийг нэмэх, хайх, шинэчлэх, жагсаах зэрэг үндсэн үйлдлүүдийг гүйцэтгэнэ.
    /// </summary>
    public class PassengerRepository : IPassengerRepository
    {
        private readonly CheckInDbContext _context;

        /// <summary>
        /// PassengerRepository-ийн шинэ экземплярыг үүсгэнэ.
        /// </summary>
        /// <param name="context">Өгөгдлийн сангийн контекст</param>
        public PassengerRepository(CheckInDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Паспортын дугаараар зорчигчийн мэдээллийг хайж олно.
        /// </summary>
        /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
        /// <returns>Зорчигчийн мэдээлэл эсвэл null</returns>
        public PassengerDto? GetByPassport(string passportNumber)
        {
            var passenger = _context.Passengers
                .AsNoTracking()
                .FirstOrDefault(p => p.PassportNumber == passportNumber);

            return passenger == null ? null : new PassengerDto
            {
                Id = passenger.PassengerId,
                FullName = passenger.FullName,
                PassportNumber = passenger.PassportNumber,
                FlightId = passenger.FlightId,
                Status = passenger.Status
            };
        }

        /// <summary>
        /// Зорчигчийн ID-аар мэдээллийг хайж олно.
        /// </summary>
        /// <param name="id">Зорчигчийн ID</param>
        /// <returns>Зорчигчийн мэдээлэл эсвэл null</returns>
        public PassengerDto? GetById(int id)
        {
            var passenger = _context.Passengers
                .AsNoTracking()
                .FirstOrDefault(p => p.PassengerId == id);

            return passenger == null ? null : new PassengerDto
            {
                Id = passenger.PassengerId,
                FullName = passenger.FullName,
                PassportNumber = passenger.PassportNumber,
                FlightId = passenger.FlightId,
                Status = passenger.Status
            };
        }

        /// <summary>
        /// Зорчигчийн төлөвийг шинэчилнэ.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <param name="newStatus">Шинэ төлөв</param>
        public void UpdateStatus(int passengerId, PassengerStatus newStatus)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.PassengerId == passengerId);
            if (passenger != null)
            {
                passenger.Status = newStatus;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Шинэ зорчигчийн мэдээллийг өгөгдлийн санд нэмнэ.
        /// </summary>
        /// <param name="dto">Зорчигчийн DTO мэдээлэл</param>
        public void Add(PassengerDto dto)
        {
            var passenger = new Passenger
            {
                FullName = dto.FullName,
                PassportNumber = dto.PassportNumber,
                FlightId = dto.FlightId,
                Status = dto.Status
            };

            _context.Passengers.Add(passenger);
            _context.SaveChanges();

            dto.Id = passenger.PassengerId;
        }

        /// <summary>
        /// Бүх зорчигчийн мэдээллийн жагсаалтыг авна.
        /// </summary>
        /// <returns>Зорчигчдын DTO жагсаалт</returns>
        public List<PassengerDto> GetAll()
        {
            return _context.Passengers
                .AsNoTracking()
                .Select(p => new PassengerDto
                {
                    Id = p.PassengerId,
                    FullName = p.FullName,
                    PassportNumber = p.PassportNumber,
                    FlightId = p.FlightId,
                    Status = p.Status
                })
                .ToList();
        }
    }
}
