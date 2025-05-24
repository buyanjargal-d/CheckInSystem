namespace CheckInServer.API.Controllers
{
    using CheckInSystem.Business.Interfaces;
    using CheckInSystem.Data;
    using CheckInSystem.DTO;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// FlightController ангилал нь нислэгийн мэдээлэлтэй холбоотой API үйлдлүүдийг гүйцэтгэнэ.
    /// Энэ контроллер нь нислэгийн жагсаалт авах, нислэгийн төлөв шинэчлэх, нислэгийн дэлгэрэнгүй мэдээлэл авах зэрэг үйлдлүүдийг агуулна.
    /// </summary>
    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
        /// <summary>
        /// Нислэгийн төлөвийн үйлчилгээний объект.
        /// </summary>
        private readonly IFlightStatusService _service;
        /// <summary>
        /// Өгөгдлийн сангийн контекст.
        /// </summary>
        private readonly CheckInDbContext _db;

        /// <summary>
        /// FlightController-ийн шинэ жишээг үүсгэнэ.
        /// </summary>
        /// <param name="service">Нислэгийн төлөвийн үйлчилгээ.</param>
        /// <param name="db">Өгөгдлийн сангийн контекст.</param>
        public FlightController(IFlightStatusService service, CheckInDbContext db)
        {
            _service = service;
            _db = db;
        }

        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх API.
        /// </summary>
        /// <param name="dto">Нислэгийн төлөв шинэчлэх DTO объект.</param>
        /// <returns>Амжилттай бол 200 OK хариу буцаана.</returns>
        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] FlightStatusUpdateDto dto)
        {
            try
            {
                Console.WriteLine($"API: Updating flight {dto.FlightId} to status {dto.NewStatus}");
                await _service.UpdateStatus(dto.FlightId, dto.NewStatus);
                Console.WriteLine($"API: Successfully updated flight {dto.FlightId}");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return StatusCode(500, $"Error updating flight status: {ex.Message}");
            }
        }

        /// <summary>
        /// Бүх нислэгийн жагсаалтыг авах API.
        /// </summary>
        /// <returns>Нислэгийн жагсаалт бүхий 200 OK хариу буцаана.</returns>
        [HttpGet]
        public IActionResult GetAllFlights()
        {
            try
            {
                var flights = _db.Flights.ToList();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Flights error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Нислэгийн ID-аар нислэгийн дэлгэрэнгүй мэдээлэл авах API.
        /// </summary>
        /// <param name="id">Нислэгийн давтагдашгүй дугаар.</param>
        /// <returns>Нислэг олдвол 200 OK, олдохгүй бол 404 NotFound хариу буцаана.</returns>
        [HttpGet("{id}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _db.Flights.FirstOrDefault(f => f.Id == id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
        [HttpGet("search")]
        public IActionResult SearchByNumber([FromQuery] string number)
        {
            var results = _db.Flights
                .Where(f => f.FlightNumber.Contains(number))
                .ToList();

            return Ok(results);
        }
    }
}