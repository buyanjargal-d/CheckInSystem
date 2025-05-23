using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    /// <summary>
    /// Нислэгийн төлөвийн үйлчилгээний интерфейс.
    /// Энэ интерфейс нь нислэгийн төлөвийг шинэчлэх үйлдлийг тодорхойлно.
    /// </summary>
    public interface IFlightStatusService
    {
        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх.
        /// </summary>
        /// <param name="flightId">Нислэгийн давтагдашгүй дугаар</param>
        /// <param name="newStatus">Шинэ төлөв</param>
        Task UpdateStatus(int flightId, string newStatus);
        //void UpdateStatus(int flightId, string newStatus);
    }
}
