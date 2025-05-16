using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    /// <summary>
    /// Нислэгийн мэдээлэл дамжуулах үйлчилгээний интерфейс.
    /// Энэ интерфейс нь нислэгийн төлөвийн мэдээллийг мэдэгдэх зориулалттай.
    /// </summary>
    public interface IFlightNotifier
    {
        /// <summary>
        /// Нислэгийн төлөвийн мэдээллийг асинхрон байдлаар мэдэгдэнэ.
        /// </summary>
        /// <param name="flightId">Нислэгийн дугаар</param>
        /// <param name="status">Нислэгийн төлөв</param>
        /// <returns>Task объект буцаана</returns>
        Task NotifyFlightStatusAsync(int flightId, string status);
    }

}
