namespace CheckInServer.API.Controllers
{
    using CheckInSystem.Business.Interfaces;
    using CheckInSystem.DTO;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightStatusService _service;
        public FlightController(IFlightStatusService service) => _service = service;

        [HttpPost("status")]
        public IActionResult UpdateStatus([FromBody] FlightStatusUpdateDto dto)
        {
            _service.UpdateStatus(dto.FlightId, dto.NewStatus);
            return Ok();
        }
    }
}
