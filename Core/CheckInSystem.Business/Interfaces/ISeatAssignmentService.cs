using CheckInSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    /// <summary>
    /// Суудал оноох үйлчилгээний интерфейс.
    /// Энэ интерфейс нь зорчигчдод суудал оноох, боломжит суудлуудыг авах, бүх суудлуудыг авах,
    /// мөн зорчигчийн суудлын мэдээллийг авах зэрэг суудалтай холбоотой үндсэн үйлдлүүдийг тодорхойлно.
    /// </summary>
    public interface ISeatAssignmentService
    {
        /// <summary>
        /// Зорчигчид суудал оноох асинхрон функц.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн дугаар</param>
        /// <param name="seatId">Суудлын дугаар</param>
        /// <returns>Оноох үйлдэл амжилттай болсон эсэхийг илэрхийлэх boolean утга</returns>
        Task<bool> AssignSeatAsync(int passengerId, int seatId);

        /// <summary>
        /// Нислэгийн боломжит (чөлөөтэй) суудлуудыг авах функц.
        /// </summary>
        /// <param name="flightId">Нислэгийн дугаар</param>
        /// <returns>Боломжит суудлуудын жагсаалт</returns>
        List<SeatDto> GetAvailableSeats(int flightId);

        /// <summary>
        /// Нислэгийн бүх суудлуудыг авах функц.
        /// </summary>
        /// <param name="flightId">Нислэгийн дугаар</param>
        /// <returns>Бүх суудлуудын жагсаалт</returns>
        List<SeatDto> GetAllSeats(int flightId);

        /// <summary>
        /// Зорчигчийн суудлын мэдээллийг авах функц.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн дугаар</param>
        /// <returns>Суудлын мэдээлэл (байхгүй бол null)</returns>
        SeatDto? GetSeatByPassenger(int passengerId);
    }
}
