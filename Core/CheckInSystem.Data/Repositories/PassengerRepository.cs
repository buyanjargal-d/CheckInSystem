using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;
using Microsoft.EntityFrameworkCore;

namespace CheckInSystem.Data.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly CheckInDbContext _context;

        public PassengerRepository(CheckInDbContext context)
        {
            _context = context;
        }

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

        public void UpdateStatus(int passengerId, PassengerStatus newStatus)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.PassengerId == passengerId);
            if (passenger != null)
            {
                passenger.Status = newStatus;

                //Console.WriteLine($"[DEBUG] Updating status for passenger {passengerId} to {newStatus}");

                _context.SaveChanges();
            }
        }


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
