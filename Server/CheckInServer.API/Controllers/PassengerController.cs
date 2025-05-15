using Microsoft.AspNetCore.Mvc;
using CheckInSystem.Data;
using CheckInSystem.Data.Repositories;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.DTO;


namespace CheckInServer.API.Controllers
{
    [ApiController]
    [Route("api/passengers")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepository _repository;
        private readonly CheckInDbContext _db;

        public PassengerController(IPassengerRepository repository, CheckInDbContext db)
        {
            _repository = repository;
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _repository.GetAll();
            return Ok(all);
        }

        [HttpGet("{passportNumber}")]
        public IActionResult GetByPassport(string passportNumber)
        {
            var passenger = _repository.GetByPassport(passportNumber);
            return passenger is null ? NotFound() : Ok(passenger);
        }

        //[HttpPost]
        //public IActionResult Add([FromBody] PassengerDto dto)
        //{
        //    _repository.Add(dto);
        //    return CreatedAtAction(nameof(GetByPassport), new { passportNumber = dto.PassportNumber }, dto);
        //}

        [HttpGet("boarding-pass/{passportNumber}")]
        public IActionResult GetBoardingPass(string passportNumber)
        {
            var passenger = _repository.GetByPassport(passportNumber);
            if (passenger == null)
                return NotFound(new { message = "Passenger not found" });

            var flight = _db.Flights.FirstOrDefault(f => f.Id == passenger.FlightId);
            if (flight == null)
                return NotFound(new { message = "Flight not found" });

            var seat = _db.Seats.FirstOrDefault(s => s.PassengerId == passenger.Id);
            if (seat == null)
                return NotFound(new { message = "Seat not assigned" });

            var boardingPass = new
            {
                passenger.FullName,
                passenger.PassportNumber,
                FlightNumber = flight.FlightNumber,
                FlightStatus = flight.Status,
                SeatNumber = seat.SeatNumber,
                DepartureTime = flight.DepartureTime.ToString("yyyy-MM-dd HH:mm")
            };

            return Ok(boardingPass);
        }

    }
}
