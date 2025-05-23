using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CheckInSystem.DTO;

// FlightStatusUpdateDto класс нь нислэгийн төлөвийг шинэчлэхэд ашиглагдана.
public class FlightStatusUpdateDto
{
    // FlightId нь нислэгийн давтагдашгүй дугаар юм.
    public int FlightId { get; set; }

    // NewStatus нь нислэгийн шинэ төлөвийг илэрхийлнэ.
    //[JsonPropertyName("status")]
    public string NewStatus { get; set; } = string.Empty;
}

