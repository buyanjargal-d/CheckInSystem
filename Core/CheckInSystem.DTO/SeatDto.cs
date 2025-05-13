using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

public class SeatDto
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public bool IsOccupied { get; set; }
    public int? PassengerId { get; set; } // null if unassigned
}

