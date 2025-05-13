namespace CheckInSystem.API.Controllers
{
    using CheckInSystem.Business.Interfaces;
    using CheckInSystem.DTO;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/seats")]
    public class SeatController : ControllerBase
    {
        private readonly ISeatAssignmentService _service;

        public SeatController(ISeatAssignmentService service) => _service = service;

        [HttpGet("available")]
        public IActionResult GetAvailable(int flightId) => Ok(_service.GetAvailableSeats(flightId));

        [HttpPost("assign")]
        public IActionResult Assign([FromBody] AssignSeatRequestDto dto)
        {
            Console.WriteLine($"Received assignment request: Passenger {dto.PassengerId}, Seat {dto.SeatId}");
            return _service.AssignSeat(dto.PassengerId, dto.SeatId)
                ? Ok(new { status = "assigned" })
                : Conflict(new { status = "seat_taken" });

        }
    }
}