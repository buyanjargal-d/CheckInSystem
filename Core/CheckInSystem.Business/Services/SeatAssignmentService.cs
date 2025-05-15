using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Repositories;
using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;

public class SeatAssignmentService : ISeatAssignmentService
{
    private readonly ISeatRepository _seatRepo;
    private readonly IPassengerRepository _passengerRepo;
    private readonly ISocketNotifier _socketNotifier;
    private readonly IFlightNotifier _flightNotifier;
    private readonly ISeatNotifier _seatNotifier;

    public SeatAssignmentService(
        ISeatRepository seatRepo,
        IPassengerRepository passengerRepo,
        ISocketNotifier socketNotifier,
        ISeatNotifier seatNotifier,
        IFlightNotifier flightNotifier)
    {
        _seatRepo = seatRepo;
        _passengerRepo = passengerRepo;
        _socketNotifier = socketNotifier;
        _seatNotifier = seatNotifier;
        _flightNotifier = flightNotifier;
    }

    //public bool LockSeat(int seatId)
    //{
    //    var success = _seatRepo.LockSeat(seatId);
    //    if (success)
    //        _socketNotifier.NotifySeatLocked(seatId);
    //    return success;
    //}

    public async Task<bool> AssignSeatAsync(int passengerId, int seatId)
    {
        //Console.WriteLine("AssignSeatAsync called with seatId: " + seatId);

        if (!_seatRepo.IsAvailable(seatId))
            return false;

        var passenger = _passengerRepo.GetById(passengerId);
        if (passenger == null)
        {
            Console.WriteLine("Passenger not found");
            return false;
        }

        int flightId = passenger.FlightId;
        //Console.WriteLine(flightId + " fucking flightId");
        var allSeats = _seatRepo.GetAllSeatsByFlightId(flightId); 
        var existingSeat = allSeats.FirstOrDefault(s => s.PassengerId == passengerId);

        if (existingSeat != null)
        {
            Console.WriteLine($"Passenger {passengerId} is already assigned to seat {existingSeat.SeatNumber}");
            return false;
        }

        var success = _seatRepo.AssignSeat(passengerId, seatId);

        if (success)
        {

            _passengerRepo.UpdateStatus(passengerId, PassengerStatus.CheckedIn);

            var seats = _seatRepo.GetAllSeatsByFlightId(flightId).ToList();
            var assignedSeat = seats.FirstOrDefault(s => s.SeatId == seatId);

            if (assignedSeat != null)
            {
                Console.WriteLine("Broadcasting assigned seat via SignalR...");
                await _seatNotifier.NotifySeatAssignedAsync(assignedSeat);
                _socketNotifier.NotifySeatAssigned(seatId, passengerId);
            }
            else
            {
                Console.WriteLine("SeatDto is still null after fetch");
            }
        }

        return success;
    }

    //public void UnlockSeat(int seatId)
    //{
    //    _seatRepo.UnlockSeat(seatId);
    //    _socketNotifier.NotifySeatUnlocked(seatId);
    //}

    public List<SeatDto> GetAvailableSeats(int flightId)
    {
        return _seatRepo.GetAvailableSeats(flightId);
    }

    public List<SeatDto> GetAllSeats(int flightId)
    {
        return _seatRepo.GetAllSeatsByFlightId(flightId).ToList();
    }

    public SeatDto? GetSeatByPassenger(int passengerId)
    {
        return _seatRepo.GetSeatByPassengerId(passengerId);
    }

}
