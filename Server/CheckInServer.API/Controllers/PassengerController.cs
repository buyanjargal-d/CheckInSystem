namespace CheckInServer.API.Controllers
{
    using CheckInSystem.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/passengers")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepository _repo;
        public PassengerController(IPassengerRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult GetByPassport(string passportNumber)
        {
            var passenger = _repo.GetByPassport(passportNumber);
            return passenger is not null ? Ok(passenger) : NotFound();
        }
    }
}
