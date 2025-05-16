using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Data.Interfaces
{
    /// <summary>
    /// Нислэгийн мэдээллийн сангийн репозиторийн интерфейс.
    /// </summary>
    public interface IFlightRepository
    {
        /// <summary>
        /// Нислэгийн төлөвийг шинэчлэх.
        /// </summary>
        /// <param name="flightId">Нислэгийн давтагдашгүй дугаар.</param>
        /// <param name="status">Шинэ төлөв.</param>
        void UpdateStatus(int flightId, string status);
    }
}
