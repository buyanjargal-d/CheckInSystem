using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;

namespace CheckInSystem.Business.Services
{
    public class FlightStatusService
    {
        private readonly IFlightRepository _repo;
        private readonly IFlightNotifier _notifier;

        public FlightStatusService(IFlightRepository repo, IFlightNotifier notifier)
        {
            _repo = repo;
            _notifier = notifier;
        }

        public void UpdateStatus(int flightId, string newStatus)
        {
            _repo.UpdateStatus(flightId, newStatus);
            _notifier.NotifyFlightStatusAsync(flightId, newStatus);
        }
    }
}
