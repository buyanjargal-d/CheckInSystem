using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;


namespace CheckInSystem.Data.Interfaces
{
    public interface IPassengerRepository
    {
        PassengerDto? GetByPassport(string passportNumber);
        PassengerDto? GetById(int id);
        void UpdateStatus(int passengerId, PassengerStatus newStatus);
        void Add(PassengerDto dto);
        List<PassengerDto> GetAll();
    }
}
