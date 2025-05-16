using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

// AssignSeatRequestDto ангилал нь зорчигчид суудал оноох хүсэлтийн өгөгдлийг хадгална.
public class AssignSeatRequestDto
{
    // Зорчигчийн давтагдашгүй дугаар
    public int PassengerId { get; set; }
    // Оноох суудлын давтагдашгүй дугаар
    public int SeatId { get; set; }
}
