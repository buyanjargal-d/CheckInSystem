namespace CheckInSystem.Data.Models;

public class Seat
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public bool IsOccupied { get; set; }

    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;
}
