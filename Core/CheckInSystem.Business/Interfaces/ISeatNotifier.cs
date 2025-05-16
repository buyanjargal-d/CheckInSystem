using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    /// <summary>
    /// Суудал оноосон тухай мэдэгдэл илгээх интерфейс.
    /// Энэ интерфейс нь суудал оноох үед мэдэгдэл илгээх зориулалттай.
    /// </summary>
    public interface ISeatNotifier
    {
        /// <summary>
        /// Суудал оноосон тухай асинхрон мэдэгдэл илгээх.
        /// </summary>
        /// <param name="seat">Оноосон суудлын мэдээлэл.</param>
        /// <returns>Task объект буцаана.</returns>
        Task NotifySeatAssignedAsync(SeatDto seat);
    }

}