namespace CheckInSystem.Data.Models;

public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Status { get; set; } = "CheckingIn";
    public DateTime DepartureTime { get; set; }

    public List<Seat> Seats { get; set; } = new();
}
