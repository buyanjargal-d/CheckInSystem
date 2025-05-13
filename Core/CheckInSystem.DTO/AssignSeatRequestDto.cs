using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

public class AssignSeatRequestDto
{
    public int PassengerId { get; set; }
    public int SeatId { get; set; }
}
