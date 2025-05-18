namespace CheckInSystem.Data.Models;

// Flight нь нислэгийн мэдээллийг хадгална.
public class Flight
{
    // Нислэгийн давтагдашгүй дугаар
    public int Id { get; set; }

    // Нислэгийн дугаар (жишээ нь: "SU123")
    public string FlightNumber { get; set; } = string.Empty;

    // Нислэгийн төлөв (жишээ нь: "CheckingIn")
    public string Status { get; set; } = "CheckingIn";

    // Нислэгийн хөөрөх цаг
    public DateTime DepartureTime { get; set; }

    // Энэ нислэгт хамаарах суудлуудын жагсаалт
    public List<Seat> Seats { get; set; } = new();
}
