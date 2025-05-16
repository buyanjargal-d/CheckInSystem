using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Models;

namespace CheckInSystem.Data.Repositories
{
    /// <summary>
    /// FlightRepository нь нислэгийн мэдээллийн сангийн үйлдлүүдийг гүйцэтгэх репозиторийн ангилал юм.
    /// </summary>
    public class FlightRepository : IFlightRepository
    {
        // CheckInDbContext нь өгөгдлийн сантай холбогдох контекст.
        private readonly CheckInDbContext _context;

        /// <summary>
        /// FlightRepository-ийн шинэ объект үүсгэх конструктор.
        /// </summary>
        /// <param name="context">Өгөгдлийн сангийн контекст.</param>
        public FlightRepository(CheckInDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх.
        /// </summary>
        /// <param name="flightId">Нислэгийн давтагдашгүй дугаар.</param>
        /// <param name="status">Шинэ төлөв.</param>
        public void UpdateStatus(int flightId, string status)
        {
            // flightId-аар нислэгийг хайж олно.
            var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);
            if (flight != null)
            {
                // Олдсон бол нислэгийн төлөвийг шинэчилнэ.
                flight.Status = status;
                // Өгөгдлийн санд хадгална.
                _context.SaveChanges();
            }
        }
    }
}
