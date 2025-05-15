namespace CheckInServer.API.Controllers
{
    using CheckInSystem.Business.Interfaces;
    using CheckInSystem.Data;
    using CheckInSystem.DTO;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightStatusService _service;
        private readonly CheckInDbContext _db;

        public FlightController(IFlightStatusService service, CheckInDbContext db)
        {
            _service = service;
            _db = db;
        }

        [HttpPost("status")]
        public IActionResult UpdateStatus([FromBody] FlightStatusUpdateDto dto)
        {
            _service.UpdateStatus(dto.FlightId, dto.NewStatus);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllFlights()
        {
            var flights = _db.Flights.ToList();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _db.Flights.FirstOrDefault(f => f.Id == id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

    }
}
