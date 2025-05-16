using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;


namespace CheckInSystem.Data.Interfaces
{
    /// <summary>
    /// Зорчигчийн өгөгдлийн сангийн үйлдлүүдийг тодорхойлсон интерфейс.
    /// </summary>
    public interface IPassengerRepository
    {
        /// <summary>
        /// Паспортын дугаараар зорчигчийг хайж олно.
        /// </summary>
        /// <param name="passportNumber">Зорчигчийн паспортын дугаар</param>
        /// <returns>Зорчигчийн мэдээлэл эсвэл null</returns>
        PassengerDto? GetByPassport(string passportNumber);

        /// <summary>
        /// ID-аар зорчигчийг хайж олно.
        /// </summary>
        /// <param name="id">Зорчигчийн ID</param>
        /// <returns>Зорчигчийн мэдээлэл эсвэл null</returns>
        PassengerDto? GetById(int id);

        /// <summary>
        /// Зорчигчийн төлөвийг шинэчилнэ.
        /// </summary>
        /// <param name="passengerId">Зорчигчийн ID</param>
        /// <param name="newStatus">Шинэ төлөв</param>
        void UpdateStatus(int passengerId, PassengerStatus newStatus);

        /// <summary>
        /// Шинэ зорчигч нэмнэ.
        /// </summary>
        /// <param name="dto">Зорчигчийн мэдээлэл</param>
        void Add(PassengerDto dto);

        /// <summary>
        /// Бүх зорчигчийн жагсаалтыг авна.
        /// </summary>
        /// <returns>Зорчигчдын жагсаалт</returns>
        List<PassengerDto> GetAll();
    }
}
