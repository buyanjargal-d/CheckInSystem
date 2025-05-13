using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.DTO;

public class SeatAssignmentService : ISeatAssignmentService
{
    private readonly ISeatRepository _seatRepo;
    private readonly IPassengerRepository _passengerRepo;

    public SeatAssignmentService(ISeatRepository seatRepo, IPassengerRepository passengerRepo)
    {
        _seatRepo = seatRepo;
        _passengerRepo = passengerRepo;
    }

    public bool LockSeat(int seatId) => _seatRepo.LockSeat(seatId);

    public bool AssignSeat(int passengerId, int seatId)
    {
        if (!_seatRepo.IsAvailable(seatId)) return false;
        return _seatRepo.AssignSeat(passengerId, seatId);
    }

    public void UnlockSeat(int seatId) => _seatRepo.UnlockSeat(seatId);

    public List<SeatDto> GetAvailableSeats(int flightId)
    {
        return _seatRepo.GetAvailableSeats(flightId);
    }
}
