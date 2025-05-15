using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;

namespace CheckInSystem.Data.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly CheckInDbContext _context;

        public FlightRepository(CheckInDbContext context)
        {
            _context = context;
        }

        public void UpdateStatus(int flightId, string status)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);
            if (flight != null)
            {
                flight.Status = status;
                _context.SaveChanges();
            }
        }
    }
}
