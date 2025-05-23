using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

// AssignSeatRequestDto ангилал нь зорчигчид суудал оноох хүсэлтийн өгөгдлийг хадгална.
public class AssignSeatRequestDto
{

    [JsonPropertyName("passportNumber")]
    public string PassportNumber { get; set; } = "";

    [JsonPropertyName("flightId")]
    public int FlightId { get; set; }

    [JsonPropertyName("seatNumber")]
    public string SeatNumber { get; set; } = "";
    // Зорчигчийн давтагдашгүй дугаар

    [JsonPropertyName("passengerId")]
    public int PassengerId { get; set; }
    // Оноох суудлын давтагдашгүй дугаар

    [JsonPropertyName("seatId")]
    public int SeatId { get; set; }
}
