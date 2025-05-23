using Microsoft.AspNetCore.Mvc;
using CheckInSystem.Data;
using CheckInSystem.Data.Repositories;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.DTO;


namespace CheckInServer.API.Controllers
{
    /// <summary>
    /// PassengerController нь зорчигчийн мэдээлэлтэй холбоотой API үйлдлүүдийг гүйцэтгэх контроллер юм.
    /// Энэ контроллер нь зорчигчийн жагсаалт авах, паспортын дугаараар хайх, зорчигчийн суудал болон нислэгийн мэдээлэл бүхий суудлын тасалбар авах зэрэг үйлдлүүдийг агуулна.
    /// </summary>
    [ApiController]
    [Route("api/passengers")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepository _repository;
        private readonly CheckInDbContext _db;

        /// <summary>
        /// PassengerController-ийн шинэ жишээг үүсгэх конструктор.
        /// </summary>
        /// <param name="repository">Зорчигчийн өгөгдлийн сангийн репозиторийн интерфейс.</param>
        /// <param name="db">CheckInDbContext өгөгдлийн сангийн контекст.</param>
        public PassengerController(IPassengerRepository repository, CheckInDbContext db)
        {
            _repository = repository;
            _db = db;
        }

        /// <summary>
        /// Бүх зорчигчийн жагсаалтыг авах GET үйлдэл.
        /// </summary>
        /// <returns>Зорчигчдын жагсаалт (PassengerDto)</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _repository.GetAll();
            return Ok(all);
        }

        /// <summary>
        /// Паспортын дугаараар зорчигчийн мэдээлэл авах GET үйлдэл.
        /// </summary>
        /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
        /// <returns>Зорчигчийн мэдээлэл эсвэл NotFound</returns>
        [HttpGet("{passportNumber}")]
        public IActionResult GetByPassport(string passportNumber)
        {
            var passenger = _repository.GetByPassport(passportNumber);
            return passenger is null ? NotFound() : Ok(passenger);
        }

        /*
        /// <summary>
        /// Шинэ зорчигч нэмэх POST үйлдэл.
        /// </summary>
        /// <param name="dto">Зорчигчийн мэдээлэл</param>
        /// <returns>Үүссэн зорчигчийн мэдээлэл</returns>
        [HttpPost]
        public IActionResult Add([FromBody] PassengerDto dto)
        {
            _repository.Add(dto);
            return CreatedAtAction(nameof(GetByPassport), new { passportNumber = dto.PassportNumber }, dto);
        }
        */

        /// <summary>
        /// Паспортын дугаараар зорчигчийн суудлын тасалбарын мэдээлэл авах GET үйлдэл.
        /// </summary>
        /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
        /// <returns>Суудлын тасалбарын мэдээлэл эсвэл NotFound</returns>
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


        /// <summary>
        /// Get passengers by flight ID.
        /// </summary>
        /// <param name="flightId">Flight ID</param>
        /// <returns>List of passengers on that flight</returns>
        //[HttpGet("by-flight/{flightId}")]
        //public IActionResult GetByFlightId(int flightId)
        //{
        //    var passengers = _repository.GetByFlightId(flightId);
        //    return Ok(passengers);
        //}

        //[HttpGet]
        //public IActionResult GetAllPassengers()
        //{
        //    var passengers = _db.Passengers
        //        .Select(p => new PassengerDto
        //        {
        //            Id = p.PassengerId,                          
        //            FullName = p.FullName,
        //            PassportNumber = p.PassportNumber,
        //            FlightId = p.FlightId,
        //            Status = p.Status
        //        }).ToList();

        //    return Ok(passengers);
        //}

        [HttpGet("by-flight/{flightId}")]
        public IActionResult GetPassengersByFlight(int flightId)
        {
            var passengers = _db.Passengers
                .Where(p => p.FlightId == flightId)
                .Select(p => new PassengerDto
                {
                    Id = p.PassengerId,
                    FullName = p.FullName,
                    PassportNumber = p.PassportNumber,
                    FlightId = p.FlightId,
                    Status = p.Status
                })
                .ToList();

            return Ok(passengers);
        }


        //[HttpGet("by-flight/{flightId}")]
        //public IActionResult GetByFlightId(int flightId)
        //{
        //    var passengers = _db.Passengers
        //        .Where(p => p.FlightId == flightId)
        //        .ToList();

        //    if (passengers == null || passengers.Count == 0)
        //        return NotFound();

        //    return Ok(passengers);
        //}

    }
}
