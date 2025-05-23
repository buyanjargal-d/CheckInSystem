using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;

namespace CheckInSystem.Business.Services
{
    /// <summary>
    /// Нислэгийн төлөвийн үйлчилгээ.
    /// Энэ класс нь нислэгийн төлөвийг шинэчилж, шинэ төлөвийг мэдэгдэнэ.
    /// </summary>
    public class FlightStatusService : IFlightStatusService
    {
        private readonly IFlightRepository _repo;
        private readonly IFlightNotifier _notifier;

        /// <summary>
        /// FlightStatusService-ийн constructor функц.
        /// </summary>
        /// <param name="repo">Нислэгийн мэдээллийн сангийн репозитор</param>
        /// <param name="notifier">Нислэгийн төлөвийн мэдэгдэл дамжуулагч</param>
        public FlightStatusService(IFlightRepository repo, IFlightNotifier notifier)
        {
            _repo = repo;
            _notifier = notifier;
        }

        /// <summary>
        /// Нислэгийн төлөвийг шинэчилж, шинэ төлөвийг асинхрон байдлаар мэдэгдэнэ.
        /// </summary>
        /// <param name="flightId">Нислэгийн давтагдашгүй дугаар</param>
        /// <param name="newStatus">Шинэ төлөв</param>
        public async Task UpdateStatus(int flightId, string newStatus)
        {
            _repo.UpdateStatus(flightId, newStatus);
            await _notifier.NotifyFlightStatusAsync(flightId, newStatus); // Awaited
        }
        //public void UpdateStatus(int flightId, string newStatus)
        //{
        //    // Нислэгийн төлөвийг мэдээллийн санд шинэчилнэ
        //    _repo.UpdateStatus(flightId, newStatus);
        //    // Шинэ төлөвийг асинхрон байдлаар мэдэгдэнэ
        //    _notifier.NotifyFlightStatusAsync(flightId, newStatus);
        //}

    }
}
