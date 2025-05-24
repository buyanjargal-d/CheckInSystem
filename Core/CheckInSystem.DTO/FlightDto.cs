using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using CheckInSystem.DTO.Enums;

namespace CheckInSystem.DTO;

/// <summary>
/// FlightDto нь нислэгийн мэдээллийг илэрхийлэх дата дамжуулах объект юм.
/// Энэ класс нь нислэгийн ID, нислэгийн дугаар, нислэгийн төлөв, хөөрөх цаг зэрэг мэдээллийг агуулна.
/// </summary>
public class FlightDto
{
    /// <summary>
    /// Нислэгийн давтагдашгүй дугаар (ID)
    /// </summary>

    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Нислэгийн дугаар (жишээ нь: OM123)
    /// </summary>


    [JsonPropertyName("flightNumber")] 
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// Нислэгийн төлөв
    /// </summary>
    //public FlightStatus Status { get; set; } = FlightStatus.CheckingIn;

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    /// <summary>
    /// Нислэгийн хөөрөх цаг (DateTime төрлөөр)
    /// </summary>


    [JsonPropertyName("departureTime")]
    public DateTime DepartureTime { get; set; }
}
