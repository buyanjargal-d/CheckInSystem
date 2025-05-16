using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.Data.Repositories;
using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;

/// <summary>
/// Суудал оноох үйлчилгээний класс.
/// Энэ класс нь зорчигчдод суудал оноох, боломжит болон бүх суудлуудыг авах,
/// зорчигчийн суудлын мэдээллийг авах зэрэг суудалтай холбоотой үндсэн үйлдлүүдийг гүйцэтгэнэ.
/// </summary>
public class SeatAssignmentService : ISeatAssignmentService
{
    // Суудлын репозиторийг хадгалах хувьсагч
    private readonly ISeatRepository _seatRepo;
    // Зорчигчийн репозиторийг хадгалах хувьсагч
    private readonly IPassengerRepository _passengerRepo;
    // Сокет мэдэгдэл илгээгчийг хадгалах хувьсагч
    private readonly ISocketNotifier _socketNotifier;
    // Нислэгийн мэдэгдэл илгээгчийг хадгалах хувьсагч
    private readonly IFlightNotifier _flightNotifier;
    // Суудлын мэдэгдэл илгээгчийг хадгалах хувьсагч
    private readonly ISeatNotifier _seatNotifier;

    /// <summary>
    /// SeatAssignmentService классын конструктор.
    /// Хэрэгтэй репозитор болон мэдэгдэл илгээгчдийг дамжуулж авна.
    /// </summary>
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

    /// <summary>
    /// Зорчигчид суудал оноох асинхрон функц.
    /// Энэ функц нь өгөгдсөн зорчигчийн дугаар болон суудлын дугаараар тухайн зорчигчид суудал оноох үйлдлийг гүйцэтгэнэ.
    /// Хэрэв суудал боломжтой бол зорчигчид оноож, суудлын мэдээллийг бусад системд мэдэгдэнэ.
    /// </summary>
    /// <param name="passengerId">Зорчигчийн дугаар</param>
    /// <param name="seatId">Суудлын дугаар</param>
    /// <returns>Оноох үйлдэл амжилттай болсон эсэхийг илэрхийлэх boolean утга</returns>
    public async Task<bool> AssignSeatAsync(int passengerId, int seatId)
    {

        Console.WriteLine("AssignSeatAsync called with seatId: " + seatId);

        // Суудал боломжтой эсэхийг шалгана
        if (!_seatRepo.IsAvailable(seatId))
            return false;

        // Зорчигчийн мэдээллийг авна
        var passenger = _passengerRepo.GetById(passengerId);
        if (passenger == null)
        {
            // Зорчигч олдсонгүй
            return false;
        }

        int flightId = passenger.FlightId;

        // Нислэгийн бүх суудлын мэдээллийг авна
        var allSeats = _seatRepo.GetAllSeatsByFlightId(flightId);

        // Зорчигчид өмнө нь суудал оноосон эсэхийг шалгана
        var existingSeat = allSeats.FirstOrDefault(s => s.PassengerId == passengerId);
        if (existingSeat != null)
        {
            // Зорчигчид аль хэдийн суудал оноосон бол
            return false;
        }

        // Суудал оноох үйлдлийг гүйцэтгэнэ
        var success = _seatRepo.AssignSeat(passengerId, seatId);
        if (success)
        {
            // Зорчигчийн төлөвийг шинэчилнэ
            _passengerRepo.UpdateStatus(passengerId, PassengerStatus.CheckedIn);

            var seats = _seatRepo.GetAllSeatsByFlightId(flightId).ToList();
            var assignedSeat = seats.FirstOrDefault(s => s.SeatId == seatId);

            // Оноосон суудлын мэдээллийг авна
            if (assignedSeat != null)
            {
                Console.WriteLine("Broadcasting assigned seat via SignalR...");
                // СигналR болон сокет ашиглан суудал оноосныг мэдэгдэнэ
                await _seatNotifier.NotifySeatAssignedAsync(assignedSeat);
                _socketNotifier.NotifySeatAssigned(seatId, passengerId);
            }
        }

        return success;
    }

    /// <summary>
    /// Нислэгийн боломжит (чөлөөтэй) суудлуудыг авах функц.
    /// Энэ функц нь өгөгдсөн нислэгийн дугаараар тухайн нислэгийн чөлөөтэй байгаа суудлуудын жагсаалтыг буцаана.
    /// </summary>
    /// <param name="flightId">Нислэгийн дугаар</param>
    /// <returns>Боломжит суудлуудын жагсаалт</returns>
    public List<SeatDto> GetAvailableSeats(int flightId)
    {
        return _seatRepo.GetAvailableSeats(flightId);
    }

    /// <summary>
    /// Нислэгийн бүх суудлуудыг авах функц.
    /// Энэ функц нь өгөгдсөн нислэгийн дугаараар тухайн нислэгийн бүх суудлуудын жагсаалтыг буцаана.
    /// </summary>
    /// <param name="flightId">Нислэгийн дугаар</param>
    /// <returns>Бүх суудлуудын жагсаалт</returns>
    public List<SeatDto> GetAllSeats(int flightId)
    {
        return _seatRepo.GetAllSeatsByFlightId(flightId).ToList();
    }

    /// <summary>
    /// Зорчигчийн суудлын мэдээллийг авах функц.
    /// Энэ функц нь өгөгдсөн зорчигчийн дугаараар тухайн зорчигчид оноогдсон суудлын мэдээллийг буцаана.
    /// </summary>
    /// <param name="passengerId">Зорчигчийн дугаар</param>
    /// <returns>Суудлын мэдээлэл (байхгүй бол null)</returns>
    public SeatDto? GetSeatByPassenger(int passengerId)
    {
        return _seatRepo.GetSeatByPassengerId(passengerId);
    }
}
