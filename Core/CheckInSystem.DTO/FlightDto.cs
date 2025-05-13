using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CheckInSystem.DTO.Enums;

namespace CheckInSystem.DTO;

public class FlightDto
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public FlightStatus Status { get; set; } = FlightStatus.CheckingIn;
    public DateTime DepartureTime { get; set; }
}

