using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CheckInSystem.DTO.Enums;

namespace CheckInSystem.DTO;

public class PassengerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public int FlightId { get; set; }
    public PassengerStatus Status { get; set; } = PassengerStatus.Booked;
}


