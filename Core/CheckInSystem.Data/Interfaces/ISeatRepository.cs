using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Data.Interfaces
{
    /// <summary>
    /// Суудлын репозиторийн интерфейс.
    /// Энэ интерфейс нь нислэгийн суудлуудыг авах, оноох, шалгах зэрэг үйлдлүүдийг тодорхойлно.
    /// </summary>
    public interface ISeatRepository
    {
        /// <summary>
        /// Тухайн нислэгийн боломжит (чөлөөтэй) суудлуудыг буцаана.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Суудлын DTO жагсаалт</returns>
        List<SeatDto> GetAvailableSeats(int flightId);

        /// <summary>
        /// Суудлыг зорчигчид оноох.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <param name="seatId">Суудлын ID</param>
        /// <returns>Амжилттай оноосон эсэх</returns>
        bool AssignSeat(int passengerId, int seatId);

        //bool LockSeat(int seatId);
        //void UnlockSeat(int seatId);

        /// <summary>
        /// Суудал чөлөөтэй эсэхийг шалгана.
        /// </summary>
        /// <param name="seatId">Суудлын ID</param>
        /// <returns>Чөлөөтэй бол true</returns>
        bool IsAvailable(int seatId);

        /// <summary>
        /// Нислэгийн бүх суудлыг буцаана (object төрөлтэй).
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Суудлын жагсаалт</returns>
        IEnumerable<object> GetSeatsByFlightId(int flightId);

        /// <summary>
        /// Нислэгийн бүх суудлыг SeatDto хэлбэрээр буцаана.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>SeatDto жагсаалт</returns>
        IEnumerable<SeatDto> GetAllSeatsByFlightId(int flightId);

        /// <summary>
        /// Зорчигчийн ID-аар суудлыг буцаана.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <returns>SeatDto эсвэл null</returns>
        SeatDto? GetSeatByPassengerId(int passengerId);
    }
}
