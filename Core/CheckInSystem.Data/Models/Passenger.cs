using CheckInSystem.DTO.Enums;

namespace CheckInSystem.Data.Models
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public int FlightId { get; set; }
        public PassengerStatus Status { get; set; }

        public Flight Flight { get; set; } = null!;
        public ICollection<Seat>? Seats { get; set; }
    }
}
