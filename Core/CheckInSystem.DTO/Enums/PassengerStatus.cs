using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO.Enums;

/// <summary>
/// PassengerStatus нь зорчигчийн бүртгэлийн систем дэх төлөвийг илэрхийлнэ.
/// </summary>
public enum PassengerStatus
{
    /// <summary>
    /// Зорчигч захиалга хийсэн (Бүртгэгдсэн).
    /// </summary>
    Booked,
    /// <summary>
    /// Зорчигч бүртгэл хийсэн (Check-in хийсэн).
    /// </summary>
    CheckedIn,
    /// <summary>
    /// Зорчигч онгоцонд суусан.
    /// </summary>
    Boarded,
    /// <summary>
    /// Захиалга цуцлагдсан.
    /// </summary>
    Cancelled
}

