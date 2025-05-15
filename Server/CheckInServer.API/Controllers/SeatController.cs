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
        public async Task<IActionResult> Assign([FromBody] AssignSeatRequestDto dto)
        {
            var existingSeat = _service.GetSeatByPassenger(dto.PassengerId);
            if (existingSeat != null)
            {
                return Conflict(new
                {
                    status = "passenger_already_assigned",
                    message = $"Passenger {dto.PassengerId} is already assigned to seat {existingSeat.SeatNumber}"
                });
            }

            var success = await _service.AssignSeatAsync(dto.PassengerId, dto.SeatId);

            return success
                ? Ok(new { status = "assigned" })
                : Conflict(new { status = "seat_taken" });
        }



        [HttpGet("all")]
        public IActionResult GetAll(int flightId)
        {
            return Ok(_service.GetAllSeats(flightId));
        }

        //[HttpPost("lock/{seatId}")]
        //public IActionResult Lock(int seatId)
        //{
        //    return _service.LockSeat(seatId)
        //        ? Ok(new { status = "locked" })
        //        : Conflict(new { status = "failed" });
        //}

        //[HttpPost("unlock/{seatId}")]
        //public IActionResult Unlock(int seatId)
        //{
        //    _service.UnlockSeat(seatId);
        //    return Ok(new { status = "unlocked" });
        //}


        //[HttpGet("test-seat-signalr")]
        //public async Task<IActionResult> TestSignalRSeat([FromServices] ISeatNotifier notifier)
        //{
        //    var seat = new SeatDto
        //    {
        //        SeatId = 206,
        //        SeatNumber = "Z99",
        //        IsOccupied = true,
        //        PassengerId = 102
        //    };

        //    Console.WriteLine("🚀 Manually invoking SignalR seat notifier...");
        //    await notifier.NotifySeatAssignedAsync(seat);
        //    return Ok("Sent test seat to SignalR");
        //}

    }
}