using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.DTO;

/// <summary>
/// FlightStatus нь нислэгийн төлөвийг илэрхийлэх enum төрөл юм.
/// Энэ enum нь дараах төлвүүдийг агуулна:
/// - CheckingIn: Нислэгт бүртгүүлж байна
/// - Boarding: Онгоцонд сууж байна
/// - Departed: Нислэг хөөрсөн
/// - Cancelled: Нислэг цуцлагдсан
/// </summary>
public enum FlightStatus
{
    /// <summary>
    /// Нислэгт бүртгүүлж байна
    /// </summary>
    CheckingIn,
    /// <summary>
    /// Онгоцонд сууж байна
    /// </summary>
    Boarding,
    /// <summary>
    /// Нислэг хөөрсөн
    /// </summary>
    Departed,
    /// <summary>
    /// Нислэг цуцлагдсан
    /// </summary>
    Cancelled
}
