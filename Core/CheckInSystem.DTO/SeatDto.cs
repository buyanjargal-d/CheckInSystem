using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

/// <summary>
/// SeatDto нь суудлын мэдээллийг илэрхийлэх DTO (Data Transfer Object) класс юм.
/// Энэ класс нь суудлын ID, суудлын дугаар, тухайн суудал эзэлсэн эсэх, зорчигчийн ID зэрэг мэдээллийг агуулна.
/// </summary>
public class SeatDto
{
    /// <summary>
    /// Суудлын давтагдашгүй ID.
    /// </summary>
    public int SeatId { get; set; }

    /// <summary>
    /// Суудлын дугаар (жишээ нь: "A1", "B2" гэх мэт).
    /// </summary>
    public string SeatNumber { get; set; } = string.Empty;

    /// <summary>
    /// Суудал эзэлсэн эсэхийг илэрхийлнэ. True бол суудал эзэлсэн, False бол сул.
    /// </summary>
    public bool IsOccupied { get; set; }

    /// <summary>
    /// Суудалд суусан зорчигчийн ID. Хэрвээ суудал сул бол null байна.
    /// </summary>
    public int? PassengerId { get; set; } //NULL байна
}

