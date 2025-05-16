using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CheckInSystem.DTO.Enums;

namespace CheckInSystem.DTO;

/// <summary>
/// PassengerDto нь зорчигчийн мэдээллийг илэрхийлэх DTO (Data Transfer Object) класс юм.
/// Энэ класс нь зорчигчийн үндсэн мэдээлэл болон бүртгэлийн төлөвийг хадгална.
/// </summary>
public class PassengerDto
{
    /// <summary>
    /// Зорчигчийн давтагдашгүй дугаар.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Зорчигчийн бүтэн нэр.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Зорчигчийн паспортын дугаар.
    /// </summary>
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Зорчигчийн нислэгийн давтагдашгүй дугаар.
    /// </summary>
    public int FlightId { get; set; }

    /// <summary>
    /// Зорчигчийн бүртгэлийн систем дэх төлөв.
    /// </summary>
    public PassengerStatus Status { get; set; } = PassengerStatus.Booked;
}


