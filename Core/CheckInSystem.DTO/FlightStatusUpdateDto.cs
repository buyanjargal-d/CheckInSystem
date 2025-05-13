using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

public class FlightStatusUpdateDto
{
    public int FlightId { get; set; }
    public string NewStatus { get; set; } = string.Empty;
}

