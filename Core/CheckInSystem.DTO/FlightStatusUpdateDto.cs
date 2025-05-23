using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CheckInSystem.DTO;

// FlightStatusUpdateDto класс нь нислэгийн төлөвийг шинэчлэхэд ашиглагдана.
public class FlightStatusUpdateDto
{
    // FlightId нь нислэгийн давтагдашгүй дугаар юм.

    [JsonPropertyName("flightId")]
    public int FlightId { get; set; }

    // NewStatus нь нислэгийн шинэ төлөвийг илэрхийлнэ.
    public string NewStatus { get; set; } = string.Empty;
}

