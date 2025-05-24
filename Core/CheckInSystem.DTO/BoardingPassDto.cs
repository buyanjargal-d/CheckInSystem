using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO
{
    public class BoardingPassDto
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string FlightNumber { get; set; }
        public string FlightStatus { get; set; }
        public string SeatNumber { get; set; }
        public string DepartureTime { get; set; }
    }

}
